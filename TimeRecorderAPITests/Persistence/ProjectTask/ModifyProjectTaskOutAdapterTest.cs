using FluentAssertions;
using TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters;
using TimeRecorderAPI.Factory;
using TimeRecorderAPITests.Fixtures;
using TimeRecorderDomain.DTO;
using Xunit;

namespace TimeRecorderAPITests.Persistence.ProjectTask {
    public class ModifyProjectTaskOutAdapterTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTOFixture _projectTaskDTO;
        private ProjectTaskDTO _newProjectTaskDTO;

        private readonly ModifyProjectTaskOutAdapter _modifyProjectTaskOutAdapter;

        public ModifyProjectTaskOutAdapterTest(ProjectTaskDTOFixture projectTaskDTO) {
            DataBaseFixture dataBase = new(projectTaskDTO);
            _projectTaskDTO = projectTaskDTO;

            ProjectTaskFactory factory = new(dataBase.Get());
            _modifyProjectTaskOutAdapter = new ModifyProjectTaskOutAdapter(dataBase.Get(), factory);

            _newProjectTaskDTO = new ProjectTaskDTO(null) {
                Name = "New Name",
                TimeEstimated = _projectTaskDTO.Get().TimeEstimated,
                TimeRecords = _projectTaskDTO.Get().TimeRecords,
                ProjectID = _projectTaskDTO.Get().ProjectID,
                TagIDs = _projectTaskDTO.Get().TagIDs
            };
        }

        [Fact(DisplayName = "Given a existing ID and ProjectTaskDTO, " +
                            "When replace ProjectTask, " +
                            "Then the modified ProjectTaskDTO is returned")]
        public async Task ReplaceExistingProjectTask() {
            ProjectTaskDTO? findTask = await _modifyProjectTaskOutAdapter.ReplaceTask(_projectTaskDTO.ID, _newProjectTaskDTO);

            _newProjectTaskDTO.ID = new Guid(_projectTaskDTO.ID);
            findTask.Should().BeEquivalentTo(_newProjectTaskDTO);
        }

        [Fact(DisplayName = "Given a non-existing ID and ProjectTaskDTO, " +
                            "When replace ProjectTask, " +
                            "Then null is returned")]
        public async Task ReplaceNonExistingProjectTask() {
            ProjectTaskDTO? findTask = await _modifyProjectTaskOutAdapter.ReplaceTask("non-existing-id", _newProjectTaskDTO);

            findTask.Should().BeNull();
        }
    }
}