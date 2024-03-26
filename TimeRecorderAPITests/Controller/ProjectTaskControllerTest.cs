﻿using FluentAssertions;
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
        private ProjectTaskDTO _projectTaskDTO = null!;
        private bool _created;

        private ProjectTaskController projectTaskController = null!;

        [OneTimeSetUp]
        public void OneTimeSetup() {
            _findProjectTaskInPort = new Mock<IFindProjectTaskInPort>();
            _addProjectTaskInPort = new Mock<IAddProjectTaskInPort>();

            DateTime dateTime = new(2024, 1, 1, 12,
                                    0, 0);
            _projectTaskDTO = new ProjectTaskDTO {
                ID = Guid.NewGuid(),
                Name = "Task 1",
                TimeEstimated = TimeSpan.FromHours(1),
                TimeRecords = new HashSet<TimeRecord> { new(dateTime.AddHours(-1), dateTime) },
                ProjectID = Guid.NewGuid(),
                TagIDs = new HashSet<Guid> { Guid.NewGuid() }
            };

            SetupPorts();

            projectTaskController = new ProjectTaskController(_findProjectTaskInPort.Object,
                                                              _addProjectTaskInPort.Object,
                                                              _validator);
        }

        private void SetupPorts() {
            _findProjectTaskInPort.When(() => !_created).Setup(x => x.FindTask(It.IsAny<string>()))
                                  .ReturnsAsync((ProjectTaskDTO?) null);
            _findProjectTaskInPort.When(() => _created).Setup(x => x.FindTask(_projectTaskDTO.ID.ToString()!))
                                  .ReturnsAsync(_projectTaskDTO);

            _addProjectTaskInPort.When(() => !_created).Setup(x => x.AddTask(_projectTaskDTO))
                                 .ReturnsAsync(_projectTaskDTO).Callback(() => _created = true);
            _addProjectTaskInPort.When(() => _created).Setup(x => x.AddTask(_projectTaskDTO))
                                 .ReturnsAsync((ProjectTaskDTO?) null);
        }

        [SetUp]
        public void Setup() {
            _created = false;
        }

        [Test]
        public async Task Given_a_existing_id_when_getting_a_ProjectTask_then_return_Ok_ProjectTaskDTO() {
            await projectTaskController.Post(_projectTaskDTO);
            IActionResult result = await projectTaskController.Get(_projectTaskDTO.ID.ToString()!);

            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(_projectTaskDTO);
        }

        [Test]
        public async Task Given_a_non_exist_id_when_getting_a_ProjectTask_then_return_NotFound() {
            IActionResult result = await projectTaskController.Get(_projectTaskDTO.ID.ToString()!);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task Given_a_valid_ProjectTaskDTO_when_posting_a_ProjectTask_then_return_Created_ProjectTaskDTO() {
            IActionResult result = await projectTaskController.Post(_projectTaskDTO);

            result.Should().BeOfType<CreatedAtActionResult>()
                  .Which.Value.Should().BeEquivalentTo(_projectTaskDTO);
        }

        [Test]
        public async Task Given_a_duplicated_ProjectTaskDTO_when_posting_a_ProjectTask_then_return_Conflict() {
            await projectTaskController.Post(_projectTaskDTO);
            IActionResult result = await projectTaskController.Post(_projectTaskDTO);

            result.Should().BeOfType<ConflictResult>();
        }
    }
}