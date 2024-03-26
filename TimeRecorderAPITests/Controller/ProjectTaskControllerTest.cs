﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.DTO;
using TimeRecorderDomain.Models;
using Xunit;
using ProjectTaskController = TimeRecorderAPI.Adapter.In.RestController.ProjectTaskController;

namespace TimeRecorderAPITests.Controller {
    public class ProjectTaskControllerTest {
        private static readonly ProjectTaskDTO _projectTaskDTO;

        private readonly Mock<IFindProjectTaskInPort> _findProjectTaskInPort;
        private readonly Mock<IAddProjectTaskInPort> _addProjectTaskInPort;
        private readonly Mock<IModifyProjectTaskInPort> _modifyProjectTaskInPort;
        private readonly Mock<IDeleteProjectTaskInPort> _deleteProjectTaskInPort;
        private readonly ProjectTaskDTOValidator _validator = new();
        private bool _created;

        private readonly ProjectTaskController _projectTaskController;

        static ProjectTaskControllerTest() {
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
        }

        public ProjectTaskControllerTest() {
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
                                  .Setup(x => x.FindTask(_projectTaskDTO.ID.ToString()!))
                                  .ReturnsAsync(_projectTaskDTO);

            _addProjectTaskInPort.When(() => !_created)
                                 .Setup(x => x.AddTask(_projectTaskDTO))
                                 .ReturnsAsync(_projectTaskDTO)
                                 .Callback(() => _created = true);
            _addProjectTaskInPort.When(() => _created)
                                 .Setup(x => x.AddTask(_projectTaskDTO))
                                 .ReturnsAsync((ProjectTaskDTO?) null);

            _modifyProjectTaskInPort.When(() => !_created)
                                    .Setup(x => x.ReplaceTask(It.IsAny<string>(), It.IsAny<ProjectTaskDTO>()))
                                    .ReturnsAsync((ProjectTaskDTO?) null);
            _modifyProjectTaskInPort.When(() => _created)
                                    .Setup(x => x.ReplaceTask(_projectTaskDTO.ID.ToString()!, It.IsAny<ProjectTaskDTO>()))
                                    .ReturnsAsync(_projectTaskDTO);
            
            _deleteProjectTaskInPort.When(() => !_created)
                                    .Setup(x => x.DeleteTask(It.IsAny<string>()))
                                    .ReturnsAsync(false);
            _deleteProjectTaskInPort.When(() => _created)
                                    .Setup(x => x.DeleteTask(_projectTaskDTO.ID.ToString()!))
                                    .ReturnsAsync(true);
        }

        [Fact(DisplayName = "Given a existing id " +
                            "When getting a ProjectTask " +
                            "Then return Ok ProjectTaskDTO")]
        public async Task GetProjectTaskDTOWithExistingID() {
            await _projectTaskController.Post(_projectTaskDTO);
            IActionResult result = await _projectTaskController.Get(_projectTaskDTO.ID.ToString()!);

            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(_projectTaskDTO);
        }

        [Fact(DisplayName = "Given a non exist id " +
                            "When getting a ProjectTask " +
                            "Then return NotFound")]
        public async Task GetProjectTaskDTOWithNonExistingID() {
            IActionResult result = await _projectTaskController.Get(_projectTaskDTO.ID.ToString()!);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact(DisplayName = "Given a valid ProjectTaskDTO " +
                            "When posting a ProjectTask " +
                            "Then return Created ProjectTaskDTO")]
        public async Task PostValidProjectTaskDTO() {
            IActionResult result = await _projectTaskController.Post(_projectTaskDTO);

            result.Should().BeOfType<CreatedAtActionResult>()
                  .Which.Value.Should().BeEquivalentTo(_projectTaskDTO);
        }

        [Fact(DisplayName = "Given a duplicated ProjectTaskDTO " +
                            "When posting a ProjectTask " +
                            "Then return Conflict")]
        public async Task PostDuplicatedProjectTaskDTO() {
            await _projectTaskController.Post(_projectTaskDTO);
            IActionResult result = await _projectTaskController.Post(_projectTaskDTO);

            result.Should().BeOfType<ConflictResult>();
        }

        [Fact(DisplayName = "Given a exist id and valid ProjectTaskDTO " +
                            "When putting a ProjectTask " +
                            "Then return Ok ProjectTaskDTO")]
        public async Task PutValidProjectTaskDTO() {
            await _projectTaskController.Post(_projectTaskDTO);
            _projectTaskDTO.Name = "Task 2";
            IActionResult result = await _projectTaskController.Put(_projectTaskDTO.ID.ToString()!, _projectTaskDTO);

            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(_projectTaskDTO);
        }

        [Fact(DisplayName = "Given a non-exist id and ProjectTaskDTO " +
                            "When putting a ProjectTask " +
                            "Then return NotFound")]
        public async Task PutNonExistingProjectTaskDTO() {
            IActionResult result = await _projectTaskController.Put(_projectTaskDTO.ID.ToString()!, _projectTaskDTO);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact(DisplayName = "Given a existing id " +
                            "When deleting a ProjectTask " +
                            "Then return Ok")]
        public async Task DeleteProjectTaskDTOWithExistingID() {
            await _projectTaskController.Post(_projectTaskDTO);
            IActionResult result = await _projectTaskController.Delete(_projectTaskDTO.ID.ToString()!);

            result.Should().BeOfType<OkResult>();
        }

        [Fact(DisplayName = "Given a non exist id " +
                            "When deleting a ProjectTask " +
                            "Then return NotFound")]
        public async Task DeleteProjectTaskDTOWithNonExistingID() {
            IActionResult result = await _projectTaskController.Delete(_projectTaskDTO.ID.ToString()!);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}