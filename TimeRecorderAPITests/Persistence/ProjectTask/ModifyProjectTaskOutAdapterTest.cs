﻿using FluentAssertions;
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

            _newProjectTaskDTO = new ProjectTaskDTO {
                ID = null,
                Name = "New Name",
                TimeEstimated = _projectTaskDTO.Get().TimeEstimated,
                TimeRecords = _projectTaskDTO.Get().TimeRecords,
                ProjectID = _projectTaskDTO.Get().ProjectID,
                TagIDs = _projectTaskDTO.Get().TagIDs
            };
        }

        [Fact(DisplayName = "Given a existing ID and ProjectTaskDTO with non-existing Project and Tag, " +
                            "When replace ProjectTask, " +
                            "Then the modified ProjectTaskDTO is returned without project and tag")]
        public async Task ReplaceExistingProjectTask() {
            ProjectTaskDTO? findTask = await _modifyProjectTaskOutAdapter.ReplaceTask(_projectTaskDTO.ID, _newProjectTaskDTO);

            _newProjectTaskDTO.ID = new Guid(_projectTaskDTO.ID);
            findTask.Should()
                    .BeEquivalentTo(_newProjectTaskDTO, options => options.Excluding(x => x.ID)
                                                                          .Excluding(x => x.ProjectID)
                                                                          .Excluding(x => x.TagIDs))
                    .And.Match<ProjectTaskDTO>(x => x.ID != null && x.ID.ToString()! == _projectTaskDTO.ID &&
                                                    x.ProjectID == null &&
                                                    x.TagIDs!.Count == 0);
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