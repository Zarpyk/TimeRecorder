using FluentAssertions;
using TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters;
using TimeRecorderAPI.Factory;
using TimeRecorderAPITests.Fixtures;
using TimeRecorderDomain.DTO;
using Xunit;

namespace TimeRecorderAPITests.Persistence.ProjectTask {
    public class ModifyProjectTaskOutAdapterTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTOFixture _projectTaskDTO;

        private readonly ModifyProjectTaskOutAdapter _modifyProjectTaskOutAdapter;

        public ModifyProjectTaskOutAdapterTest(ProjectTaskDTOFixture projectTaskDTO) {
            DataBaseFixture dataBase = new(projectTaskDTO);
            _projectTaskDTO = projectTaskDTO;

            ProjectFactory projectFactory = new();
            TagFactory tagFactory = new();
            ProjectTaskFactory factory = new(dataBase.Get(), projectFactory, tagFactory);
            _modifyProjectTaskOutAdapter = new ModifyProjectTaskOutAdapter(dataBase.Get(), factory);
        }

        [Fact(DisplayName = "Given a existing ID and ProjectTaskDTO, " +
                            "When replace ProjectTask, " +
                            "Then the modified ProjectTaskDTO is returned")]
        public async Task ReplaceExistingProjectTaskWithValidProjectAndTag() {
            ProjectTaskDTO newProjectTaskDTO = new() {
                ID = null,
                Name = "New Name",
                TimeEstimated = _projectTaskDTO.Get().TimeEstimated,
                TimeRecords = _projectTaskDTO.Get().TimeRecords,
                Project = _projectTaskDTO.Get().Project,
                Tags = _projectTaskDTO.Get().Tags
            };

            ProjectTaskDTO? findTask = await _modifyProjectTaskOutAdapter.Replace(_projectTaskDTO.ID, newProjectTaskDTO);

            findTask.Should().BeEquivalentTo(newProjectTaskDTO, options => options.Excluding(x => x.ID))
                    .And.Match<ProjectTaskDTO>(x => x.ID != null && x.ID.ToString()! == _projectTaskDTO.ID);
        }

        [Fact(DisplayName = "Given a non-existing ID and ProjectTaskDTO, " +
                            "When replace ProjectTask, " +
                            "Then null is returned")]
        public async Task ReplaceNonExistingProjectTask() {
            ProjectTaskDTO? findTask = await _modifyProjectTaskOutAdapter.Replace("non-existing-id", _projectTaskDTO.Get());

            findTask.Should().BeNull();
        }
    }
}