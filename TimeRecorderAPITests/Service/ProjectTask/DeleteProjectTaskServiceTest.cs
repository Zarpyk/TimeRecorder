using FluentAssertions;
using Moq;
using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Application.Service.ProjectTaskService;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Service.ProjectTask {
    public class DeleteProjectTaskServiceTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTOFixture _projectTaskDTO;

        private readonly Mock<IDeleteProjectTaskOutPort> _deleteProjectTaskOutPort;
        private readonly DeleteProjectTaskService _deleteProjectTaskService;

        public DeleteProjectTaskServiceTest(ProjectTaskDTOFixture projectTaskDTO) {
            _projectTaskDTO = projectTaskDTO;
            _deleteProjectTaskOutPort = new Mock<IDeleteProjectTaskOutPort>();

            SetupPorts();

            _deleteProjectTaskService = new DeleteProjectTaskService(_deleteProjectTaskOutPort.Object);
        }

        private void SetupPorts() {
            _deleteProjectTaskOutPort.Setup(x => x.DeleteTask(It.IsNotIn(_projectTaskDTO.ID)))
                                     .ReturnsAsync(false);
            _deleteProjectTaskOutPort.Setup(x => x.DeleteTask(_projectTaskDTO.ID))
                                     .ReturnsAsync(true);
        }

        [Fact(DisplayName = "Given a existing ID, " +
                            "When delete ProjectTask , " +
                            "Then true is returned.")]
        public async Task DeleteExistingProjectTask() {
            bool result = await _deleteProjectTaskService.Delete(_projectTaskDTO.ID);
            result.Should().BeTrue();
        }
        
        [Fact(DisplayName = "Given a non-existing ID, " +
                            "When delete ProjectTask , " +
                            "Then false is returned.")]
        public async Task DeleteNonExistingProjectTask() {
            bool result = await _deleteProjectTaskService.Delete("non-existing-id");
            result.Should().BeFalse();
        }
    }
}