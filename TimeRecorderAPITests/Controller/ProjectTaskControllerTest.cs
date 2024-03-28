using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TimeRecorderAPI.Adapter.In.RestController;
using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.DTO;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Controller {
    public class ProjectTaskControllerTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTOFixture _projectTaskDTO;

        private readonly Mock<IFindProjectTaskInPort> _findProjectTaskInPort;
        private readonly Mock<IAddProjectTaskInPort> _addProjectTaskInPort;
        private readonly Mock<IModifyProjectTaskInPort> _modifyProjectTaskInPort;
        private readonly Mock<IDeleteProjectTaskInPort> _deleteProjectTaskInPort;
        private readonly ProjectTaskDTOValidator _validator = new();
        private bool _created;

        private readonly ProjectTaskController _projectTaskController;

        public ProjectTaskControllerTest(ProjectTaskDTOFixture projectTaskDTO) {
            _projectTaskDTO = projectTaskDTO;

            _findProjectTaskInPort = new Mock<IFindProjectTaskInPort>();
            _addProjectTaskInPort = new Mock<IAddProjectTaskInPort>();
            _modifyProjectTaskInPort = new Mock<IModifyProjectTaskInPort>();
            _deleteProjectTaskInPort = new Mock<IDeleteProjectTaskInPort>();

            SetupPorts();

            _projectTaskController = new ProjectTaskController(_findProjectTaskInPort.Object,
                                                               _addProjectTaskInPort.Object,
                                                               _modifyProjectTaskInPort.Object,
                                                               _deleteProjectTaskInPort.Object,
                                                               _validator);
        }

        private void SetupPorts() {
            _findProjectTaskInPort.When(() => !_created)
                                  .Setup(x => x.FindTask(It.IsAny<string>()))
                                  .ReturnsAsync((ProjectTaskDTO?) null);
            _findProjectTaskInPort.When(() => _created)
                                  .Setup(x => x.FindTask(_projectTaskDTO.ID))
                                  .ReturnsAsync(_projectTaskDTO.Get);

            _addProjectTaskInPort.Setup(x => x.AddTask(_projectTaskDTO.Get()))
                                 .ReturnsAsync(_projectTaskDTO.Get())
                                 .Callback(() => _created = true);

            _modifyProjectTaskInPort.When(() => !_created)
                                    .Setup(x => x.ReplaceTask(It.IsAny<string>(), It.IsAny<ProjectTaskDTO>()))
                                    .ReturnsAsync((ProjectTaskDTO?) null);
            _modifyProjectTaskInPort.When(() => _created)
                                    .Setup(x => x.ReplaceTask(_projectTaskDTO.ID, It.IsAny<ProjectTaskDTO>()))
                                    .ReturnsAsync((string id, ProjectTaskDTO projectTaskDTO) => {
                                         projectTaskDTO.ID = new Guid(id);
                                         return projectTaskDTO;
                                     });

            _deleteProjectTaskInPort.When(() => !_created)
                                    .Setup(x => x.DeleteTask(It.IsAny<string>()))
                                    .ReturnsAsync(false);
            _deleteProjectTaskInPort.When(() => _created)
                                    .Setup(x => x.DeleteTask(_projectTaskDTO.ID))
                                    .ReturnsAsync(true);
        }

        [Fact(DisplayName = "Given a existing id, " +
                            "When getting a ProjectTask, " +
                            "Then Ok ProjectTaskDTO returned.")]
        public async Task GetProjectTaskDTOWithExistingID() {
            await _projectTaskController.Post(_projectTaskDTO.Get());
            IActionResult result = await _projectTaskController.Get(_projectTaskDTO.ID);

            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(_projectTaskDTO.Get());
        }

        [Fact(DisplayName = "Given a non-existing id, " +
                            "When getting a ProjectTask, " +
                            "Then NotFound is returned.")]
        public async Task GetProjectTaskDTOWithNonExistingID() {
            IActionResult result = await _projectTaskController.Get(_projectTaskDTO.ID);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact(DisplayName = "Given a valid ProjectTaskDTO, " +
                            "When posting a ProjectTask, " +
                            "Then Created ProjectTaskDTO is returned.")]
        public async Task PostValidProjectTaskDTO() {
            IActionResult result = await _projectTaskController.Post(_projectTaskDTO.Get());

            result.Should().BeOfType<CreatedAtActionResult>()
                  .Which.Value.Should().BeEquivalentTo(_projectTaskDTO.Get());
        }

        [Fact(DisplayName = "Given a existing id and valid ProjectTaskDTO, " +
                            "When putting a ProjectTask, " +
                            "Then Ok ProjectTaskDTO is returned.")]
        public async Task PutValidProjectTaskDTO() {
            await _projectTaskController.Post(_projectTaskDTO.Get());
            ProjectTaskDTO newProjectTaskDTO = new() {
                ID = null,
                Name = "New Name",
                TimeEstimated = _projectTaskDTO.Get().TimeEstimated,
                TimeRecords = _projectTaskDTO.Get().TimeRecords,
                ProjectID = _projectTaskDTO.Get().ProjectID,
                TagIDs = _projectTaskDTO.Get().TagIDs
            };

            IActionResult result = await _projectTaskController.Put(_projectTaskDTO.ID, newProjectTaskDTO);

            newProjectTaskDTO.ID = new Guid(_projectTaskDTO.ID);
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(newProjectTaskDTO);
        }

        [Fact(DisplayName = "Given a non-existing id and ProjectTaskDTO, " +
                            "When putting a ProjectTask, " +
                            "Then NotFound is returned.")]
        public async Task PutNonExistingProjectTaskDTO() {
            IActionResult result = await _projectTaskController.Put(_projectTaskDTO.ID, _projectTaskDTO.Get());

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact(DisplayName = "Given a existing id, " +
                            "When deleting a ProjectTask, " +
                            "Then Ok is returned.")]
        public async Task DeleteProjectTaskDTOWithExistingID() {
            await _projectTaskController.Post(_projectTaskDTO.Get());
            IActionResult result = await _projectTaskController.Delete(_projectTaskDTO.ID);

            result.Should().BeOfType<OkResult>();
        }

        [Fact(DisplayName = "Given a non-existing id, " +
                            "When deleting a ProjectTask, " +
                            "Then NotFound is returned.")]
        public async Task DeleteProjectTaskDTOWithNonExistingID() {
            IActionResult result = await _projectTaskController.Delete(_projectTaskDTO.ID);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}