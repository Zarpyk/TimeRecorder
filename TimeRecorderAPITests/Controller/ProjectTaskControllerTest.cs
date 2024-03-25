using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.DTO;
using TimeRecorderDomain.Models;
using ProjectTaskController = TimeRecorderAPI.Adapter.In.RestController.ProjectTaskController;

namespace TimeRecorderAPITests.Controller {
    public class ProjectTaskControllerTest {
        private Mock<IFindProjectTaskInPort> _findProjectTaskInPort = null!;
        private Mock<IAddProjectTaskInPort> _addProjectTaskInPort = null!;
        private ProjectTaskDTOValidator _validator = new();
        private Mock<ProjectTaskDTO> _projectTaskDTO = null!;
        private bool _created;

        private ProjectTaskController projectTaskController = null!;

        [OneTimeSetUp]
        public void OneTimeSetup() {
            _findProjectTaskInPort = new Mock<IFindProjectTaskInPort>();
            _addProjectTaskInPort = new Mock<IAddProjectTaskInPort>();

            DateTime dateTime = new(2024, 1, 1, 12,
                                    0, 0);
            _projectTaskDTO = new Mock<ProjectTaskDTO>(new ProjectTaskDTO {
                Name = "Task 1",
                TimeEstimated = TimeSpan.FromHours(1),
                TimeRecords = new HashSet<TimeRecord> { new(dateTime.AddHours(-1), dateTime) },
                ProjectID = Guid.NewGuid(),
                TagIDs = new HashSet<Guid> { Guid.NewGuid() }
            });
            _projectTaskDTO.SetupGet(x => x.ID).Returns(Guid.NewGuid());

            SetupPorts();

            projectTaskController = new ProjectTaskController(_findProjectTaskInPort.Object,
                                                              _addProjectTaskInPort.Object,
                                                              _validator);
        }

        private void SetupPorts() {
            _findProjectTaskInPort.Setup(x => x.FindTask(_projectTaskDTO.Object.ID.ToString()!))
                                  .ReturnsAsync(_projectTaskDTO.Object);

            _addProjectTaskInPort.When(() => !_created).Setup(x => x.AddTask(_projectTaskDTO.Object))
                                 .ReturnsAsync(_projectTaskDTO.Object).Callback(() => _created = true);
            _addProjectTaskInPort.When(() => _created).Setup(x => x.AddTask(_projectTaskDTO.Object))
                                 .ReturnsAsync((ProjectTaskDTO?) null);
        }

        [SetUp]
        public void Setup() {
            _created = false;
        }

        [Test]
        public async Task Given_a_existing_id_when_getting_a_ProjectTask_then_return_Ok_ProjectTaskDTO() {
            IActionResult result = await projectTaskController.Get(_projectTaskDTO.Object.ID.ToString()!);

            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(_projectTaskDTO.Object);
        }

        [Test]
        public async Task Given_a_non_exist_id_when_getting_a_ProjectTask_then_return_NotFound() {
            IActionResult result = await projectTaskController.Get(Guid.NewGuid().ToString());

            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task Given_a_valid_ProjectTaskDTO_when_posting_a_ProjectTask_then_return_Created_ProjectTaskDTO() {
            IActionResult result = await projectTaskController.Post(_projectTaskDTO.Object);

            result.Should().BeOfType<CreatedAtActionResult>()
                  .Which.Value.Should().BeEquivalentTo(_projectTaskDTO.Object);
        }

        [Test]
        public async Task Given_a_duplicated_ProjectTaskDTO_when_posting_a_ProjectTask_then_return_Conflict() {
            await projectTaskController.Post(_projectTaskDTO.Object);
            IActionResult result = await projectTaskController.Post(_projectTaskDTO.Object);

            result.Should().BeOfType<ConflictResult>();
        }
    }
}