using FluentAssertions;
using Moq;
using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Application.Service.ProjectTaskService;
using TimeRecorderAPI.DTO;
using TimeRecorderAPITests.Fixtures;
using TimeRecorderDomain.DTO;
using Xunit;

namespace TimeRecorderAPITests.Service.ProjectTask {
    public class ModifyProjectTaskServiceTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTOFixture _projectTaskDTO;

        private readonly Mock<IModifyProjectTaskOutPort> _modifyProjectTaskOutPort;
        private readonly ModifyProjectTaskService _modifyProjectTaskService;

        public ModifyProjectTaskServiceTest(ProjectTaskDTOFixture projectTaskDTO) {
            _projectTaskDTO = projectTaskDTO;
            _modifyProjectTaskOutPort = new Mock<IModifyProjectTaskOutPort>();

            SetupPorts();

            _modifyProjectTaskService = new ModifyProjectTaskService(_modifyProjectTaskOutPort.Object);
        }

        private void SetupPorts() {
            _modifyProjectTaskOutPort.Setup(x => x.ReplaceTask(_projectTaskDTO.ID, It.IsAny<ProjectTaskDTO>()))
                                     .ReturnsAsync((string id, ProjectTaskDTO projectTaskDTO) => {
                                          projectTaskDTO.ID = new Guid(id);
                                          return projectTaskDTO;
                                      });
        }

        [Fact(DisplayName = "Given a existing ID, " +
                            "When replace ProjectTask , " +
                            "Then the modified ProjectTask is returned.")]
        public async Task ReplaceExistingTask() {
            ProjectTaskDTO newProjectTaskDTO = new(null) {
                Name = "New Name",
                TimeEstimated = _projectTaskDTO.Get().TimeEstimated,
                TimeRecords = _projectTaskDTO.Get().TimeRecords,
                ProjectID = _projectTaskDTO.Get().ProjectID,
                TagIDs = _projectTaskDTO.Get().TagIDs
            };

            ProjectTaskDTO? projectTaskDTO = await _modifyProjectTaskService.ReplaceTask(_projectTaskDTO.ID, newProjectTaskDTO);
            newProjectTaskDTO.ID = new Guid(_projectTaskDTO.ID);
            projectTaskDTO.Should().BeEquivalentTo(newProjectTaskDTO);
        }

        [Fact(DisplayName = "Given a non-existing ID, " +
                            "When replace ProjectTask , " +
                            "Then null is returned.")]
        public async Task ReplaceNonExistingTask() {
            ProjectTaskDTO newProjectTaskDTO = new(null) {
                Name = "New Name",
                TimeEstimated = _projectTaskDTO.Get().TimeEstimated,
                TimeRecords = _projectTaskDTO.Get().TimeRecords,
                ProjectID = _projectTaskDTO.Get().ProjectID,
                TagIDs = _projectTaskDTO.Get().TagIDs
            };

            ProjectTaskDTO? projectTaskDTO = await _modifyProjectTaskService.ReplaceTask("non-existing-id", newProjectTaskDTO);
            projectTaskDTO.Should().BeNull();
        }
    }
}