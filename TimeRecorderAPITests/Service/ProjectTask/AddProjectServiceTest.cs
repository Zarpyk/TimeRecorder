using FluentAssertions;
using Moq;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Application.Service.ProjectTaskService;
using TimeRecorderAPITests.Fixtures;
using TimeRecorderDomain.DTO;
using Xunit;

namespace TimeRecorderAPITests.Service.ProjectTask {
    public class AddProjectServiceTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTOFixture _projectTaskDTO;

        private readonly Mock<IAddProjectTaskOutPort> _addProjectTaskOutPort;
        private readonly AddProjectService _addProjectService;

        public AddProjectServiceTest(ProjectTaskDTOFixture projectTaskDTO) {
            _projectTaskDTO = projectTaskDTO;
            _addProjectTaskOutPort = new Mock<IAddProjectTaskOutPort>();

            SetupPorts();

            _addProjectService = new AddProjectService(_addProjectTaskOutPort.Object);
        }

        private void SetupPorts() {
            _addProjectTaskOutPort.Setup(x => x.Add(_projectTaskDTO.Get()))
                                  .ReturnsAsync(_projectTaskDTO.Get());
        }

        [Fact(DisplayName = "Given a ProjectTaskDTO, " +
                            "When AddTask is called, " +
                            "Then ProjectTaskDTO is returned.")]
        public async Task AddNewProjectTask() {
            ProjectTaskDTO? projectTaskDTO = await _addProjectService.Add(_projectTaskDTO.Get());
            projectTaskDTO.Should().BeEquivalentTo(_projectTaskDTO.Get());
        }
    }
}