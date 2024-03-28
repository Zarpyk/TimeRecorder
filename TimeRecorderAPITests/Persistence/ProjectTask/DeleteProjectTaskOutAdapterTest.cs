using FluentAssertions;
using TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters;
using TimeRecorderAPI.DTO;
using TimeRecorderAPI.Factory;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Persistence.ProjectTask {
    public class DeleteProjectTaskOutAdapterTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTOFixture _projectTaskDTO;

        private readonly DeleteProjectTaskOutAdapter _deleteProjectTaskOutAdapter;

        public DeleteProjectTaskOutAdapterTest(ProjectTaskDTOFixture projectTaskDTO) {
            DataBaseFixture dataBase = new(projectTaskDTO);
            _projectTaskDTO = projectTaskDTO;

            ProjectTaskFactory factory = new(dataBase.Get());
            _deleteProjectTaskOutAdapter = new DeleteProjectTaskOutAdapter(dataBase.Get(), factory);
        }

        [Fact(DisplayName = "Given a existing ID, " +
                            "When delete ProjectTask, " +
                            "Then true is returned")]
        public async Task ReplaceExistingProjectTask() {
            bool delete = await _deleteProjectTaskOutAdapter.DeleteTask(_projectTaskDTO.ID);

            delete.Should().BeTrue();
        }

        [Fact(DisplayName = "Given a non-existing ID, " +
                            "When delete ProjectTask, " +
                            "Then false is returned")]
        public async Task ReplaceNonExistingProjectTask() {
            bool delete = await _deleteProjectTaskOutAdapter.DeleteTask("non-existing-id");

            delete.Should().BeFalse();
        }
    }
}