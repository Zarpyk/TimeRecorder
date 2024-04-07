using FluentAssertions;
using TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters;
using TimeRecorderAPI.Factory;
using TimeRecorderAPITests.Fixtures;
using TimeRecorderDomain.DTO;
using Xunit;

namespace TimeRecorderAPITests.Persistence.ProjectTask {
    public class AddProjectTaskOutAdapterTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTOFixture _projectTaskDTO;

        private readonly AddProjectTaskOutAdapter _addProjectTaskOutAdapter;

        public AddProjectTaskOutAdapterTest(ProjectTaskDTOFixture projectTaskDTO) {
            DataBaseFixture dataBase = new(projectTaskDTO);
            _projectTaskDTO = projectTaskDTO;

            ProjectFactory projectFactory = new();
            TagFactory tagFactory = new();
            ProjectTaskFactory factory = new(dataBase.Get(), projectFactory, tagFactory);
            _addProjectTaskOutAdapter = new AddProjectTaskOutAdapter(dataBase.Get(), factory);
        }

        [Fact(DisplayName = "Given ProjectTaskDTO, " +
                            "When add ProjectTask, " +
                            "Then ProjectTaskDTO is returned.")]
        public async Task AddProjectTaskWithValidProjectAndTag() {
            ProjectTaskDTO findTask = await _addProjectTaskOutAdapter.Add(_projectTaskDTO.Get());

            findTask.Should().BeEquivalentTo(_projectTaskDTO.Get(), options => options.Excluding(x => x.ID));
        }
    }
}