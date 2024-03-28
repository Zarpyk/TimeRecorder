using FluentAssertions;
using Moq;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Application.Service.ProjectTaskService;
using TimeRecorderAPITests.Fixtures;
using TimeRecorderDomain.DTO;
using Xunit;

namespace TimeRecorderAPITests.Service.ProjectTask {
    public class AddProjectTaskServiceTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTOFixture _projectTaskDTO;

        private readonly Mock<IAddProjectTaskOutPort> _addProjectTaskOutPort;
        private readonly AddProjectTaskService _addProjectTaskService;

        public AddProjectTaskServiceTest(ProjectTaskDTOFixture projectTaskDTO) {
            _projectTaskDTO = projectTaskDTO;
            _addProjectTaskOutPort = new Mock<IAddProjectTaskOutPort>();

            SetupPorts();

            _addProjectTaskService = new AddProjectTaskService(_addProjectTaskOutPort.Object);
        }

        private void SetupPorts() {
            _addProjectTaskOutPort.Setup(x => x.AddTask(_projectTaskDTO.Get()))
                                  .ReturnsAsync(_projectTaskDTO.Get());
        }

        [Fact(DisplayName = "Given a ProjectTaskDTO, " +
                            "When AddTask is called, " +
                            "Then ProjectTaskDTO is returned.")]
        public async Task AddNewProjectTask() {
            ProjectTaskDTO? projectTaskDTO = await _addProjectTaskService.AddTask(_projectTaskDTO.Get());
            projectTaskDTO.Should().BeEquivalentTo(_projectTaskDTO.Get());
        }
    }
}