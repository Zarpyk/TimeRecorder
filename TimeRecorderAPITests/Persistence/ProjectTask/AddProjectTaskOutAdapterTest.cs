﻿using FluentAssertions;
using TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters;
using TimeRecorderAPI.DTO;
using TimeRecorderAPI.Factory;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Persistence.ProjectTask {
    public class AddProjectTaskOutAdapterTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTOFixture _projectTaskDTO;

        private readonly AddProjectTaskOutAdapter _addProjectTaskOutAdapter;

        public AddProjectTaskOutAdapterTest(ProjectTaskDTOFixture projectTaskDTO) {
            DataBaseFixture dataBase = new(projectTaskDTO);
            _projectTaskDTO = projectTaskDTO;

            ProjectTaskFactory factory = new(dataBase.Get());
            _addProjectTaskOutAdapter = new AddProjectTaskOutAdapter(dataBase.Get(), factory);
        }

        // TODO Change this to create automatically the Project and Tag
        [Fact(DisplayName = "Given ProjectTaskDTO with non-existing Project and Tag, " +
                            "When add ProjectTask, " +
                            "Then ProjectTaskDTO without the Project and Tag is returned.")]
        public async Task FindExistingProjectTask() {
            ProjectTaskDTO newProjectTaskDTO = new() {
                ID = _projectTaskDTO.Get().ID,
                Name = _projectTaskDTO.Get().Name,
                TimeEstimated = _projectTaskDTO.Get().TimeEstimated,
                TimeRecords = _projectTaskDTO.Get().TimeRecords,
                ProjectID = Guid.Empty,
                TagIDs = new HashSet<Guid> { Guid.Empty }
            };
            
            ProjectTaskDTO findTask = await _addProjectTaskOutAdapter.AddTask(newProjectTaskDTO);

            findTask.Should()
                    .BeEquivalentTo(newProjectTaskDTO,
                                    options => options.Excluding(x => x.ID)
                                                      .Excluding(x => x.ProjectID)
                                                      .Excluding(x => x.TagIDs))
                    .And.Match<ProjectTaskDTO>(x => x.ID != null && x.ID.ToString()! != _projectTaskDTO.ID)
                    .And.Match<ProjectTaskDTO>(x => x.ProjectID == null)
                    .And.Match<ProjectTaskDTO>(x => x.TagIDs!.Count == 0);
        }
        
        // TODO Test for existing Project and Tag
    }
}