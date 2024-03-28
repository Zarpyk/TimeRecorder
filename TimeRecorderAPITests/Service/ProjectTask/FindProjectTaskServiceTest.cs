using FluentAssertions;
using Moq;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Application.Service.ProjectTaskService;
using TimeRecorderAPI.DTO;
using TimeRecorderAPITests.Fixtures;
using TimeRecorderDomain.DTO;
using Xunit;

namespace TimeRecorderAPITests.Service.ProjectTask {
    public class FindProjectTaskServiceTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTOFixture _projectTaskDTO;

        private readonly Mock<IFindProjectTaskOutPort> _findProjectTaskOutPort;
        private readonly FindProjectTaskService _findProjectTaskService;

        public FindProjectTaskServiceTest(ProjectTaskDTOFixture projectTaskDTO) {
            _projectTaskDTO = projectTaskDTO;
            _findProjectTaskOutPort = new Mock<IFindProjectTaskOutPort>();

            SetupPorts();

            _findProjectTaskService = new FindProjectTaskService(_findProjectTaskOutPort.Object);
        }

        private void SetupPorts() {
            _findProjectTaskOutPort.Setup(x => x.FindTask(It.IsAny<string>()))
                                   .ReturnsAsync((ProjectTaskDTO?) null);
            _findProjectTaskOutPort.Setup(x => x.FindTask(_projectTaskDTO.ID))
                                   .ReturnsAsync(_projectTaskDTO.Get());
        }

        [Fact(DisplayName = "Given a existing task ID, " +
                            "When FindTask is called, " +
                            "Then the task is returned.")]
        public async Task FindTaskWithExistingID() {
            ProjectTaskDTO? projectTaskDTO = await _findProjectTaskService.FindTask(_projectTaskDTO.ID);
            projectTaskDTO.Should().BeEquivalentTo(_projectTaskDTO.Get());
        }

        [Fact(DisplayName = "Given a non-existing task ID, " +
                            "When FindTask is called, " +
                            "Then null is returned.")]
        public async Task FindTaskWithNonExistingID() {
            ProjectTaskDTO? projectTaskDTO = await _findProjectTaskService.FindTask("non-existing-id");
            projectTaskDTO.Should().BeNull();
        }
    }
}