using FluentAssertions;
using TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters;
using TimeRecorderAPI.DTO;
using TimeRecorderAPI.Factory;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Persistence.ProjectTask {
    public class FindProjectTaskOutAdapterTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTOFixture _projectTaskDTO;

        private readonly FindProjectTaskOutAdapter _findProjectTaskOutAdapter;

        public FindProjectTaskOutAdapterTest(ProjectTaskDTOFixture projectTaskDTO) {
            DataBaseFixture dataBase = new(projectTaskDTO);
            _projectTaskDTO = projectTaskDTO;

            ProjectTaskFactory factory = new(dataBase.Get());
            _findProjectTaskOutAdapter = new FindProjectTaskOutAdapter(dataBase.Get(), factory);
        }

        [Fact(DisplayName = "Given a existing ID, " +
                            "When find ProjectTask, " +
                            "Then return the ProjectTaskDTO")]
        public async Task FindExistingProjectTask() {
            ProjectTaskDTO? findTask = await _findProjectTaskOutAdapter.FindTask(_projectTaskDTO.ID);

            findTask.Should().BeEquivalentTo(_projectTaskDTO.Get());
        }

        [Fact(DisplayName = "Given a non-existing ID, " +
                            "When find ProjectTask, " +
                            "Then return null")]
        public async Task FindNonExistingProjectTask() {
            ProjectTaskDTO? findTask = await _findProjectTaskOutAdapter.FindTask("non-existing-id");

            findTask.Should().BeNull();
        }
    }
}