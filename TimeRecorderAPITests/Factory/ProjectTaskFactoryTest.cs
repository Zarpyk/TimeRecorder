using FluentAssertions;
using TimeRecorderAPI.Factory;
using TimeRecorderAPI.Models;
using TimeRecorderAPITests.Fixtures;
using TimeRecorderDomain.DTO;
using Xunit;

namespace TimeRecorderAPITests.Factory {
    public class ProjectTaskFactoryTest : IClassFixture<ProjectTaskDTOFixture> {
        private readonly ProjectTaskDTO _projectTaskDTO;

        private readonly ProjectTaskFactory _projectTaskFactory;
        private readonly ProjectFactory _projectFactory = new();
        private readonly TagFactory _tagFactory = new();

        public ProjectTaskFactoryTest(ProjectTaskDTOFixture projectTaskDTOFixture) {
            DataBaseFixture dataBase = new(projectTaskDTOFixture);
            _projectTaskDTO = projectTaskDTOFixture.Get();
            
            _projectTaskFactory = new ProjectTaskFactory(dataBase.Get(), _projectFactory, _tagFactory);
        }

        [Fact(DisplayName = "Given ProjectTaskDTO with existing Project and Tag, " +
                            "When create ProjectTask, " +
                            "Then ProjectTask with Project and Tag is returned.")]
        public async Task CreateProjectTaskWithValidProjectAndTag() {
            ProjectTask? projectTask = await _projectTaskFactory.Create(_projectTaskDTO);

            projectTask.Should().NotBeNull();
            projectTask!.ID.Should().NotBe(_projectTaskDTO.ID.ToString());
            projectTask.Name.Should().Be(_projectTaskDTO.Name);
            projectTask.TimeEstimated.Should().Be(_projectTaskDTO.TimeEstimated);
            projectTask.TimeRecords.Should().BeEquivalentTo(_projectTaskDTO.TimeRecords);
            
            projectTask.Project.Should().NotBeNull();
            projectTask.Project!.Should().BeEquivalentTo(new Project {
                ID = _projectTaskDTO.Project!.ID.ToString()!,
                Name = _projectTaskDTO.Project.Name,
                Color = _projectTaskDTO.Project.Color
            });
            
            projectTask.Tags.Should().NotBeNull();
            projectTask.Tags!.Should().HaveCount(_projectTaskDTO.Tags!.Count);
            foreach (TagDTO? tag in _projectTaskDTO.Tags!) {
                projectTask.Tags.Should().ContainEquivalentOf(new Tag {
                    ID = tag!.ID.ToString()!,
                    Name = tag.Name,
                    Color = tag.Color
                });
            }
        }
        
        [Fact(DisplayName = "Given ProjectTaskDTO with non-existing Project and Tag, " +
                            "When create ProjectTask, " +
                            "Then ProjectTask with null Project and Tag is returned.")]
        public async Task CreateProjectTaskWithNonExistingProjectAndTag() {
            ProjectTaskDTO newProjectTaskDTO = _projectTaskDTO with {
                Project = new ProjectDTO { ID = Guid.Empty },
                Tags = [new TagDTO { ID = Guid.Empty }]
            };
            
            ProjectTask? projectTask = await _projectTaskFactory.Create(newProjectTaskDTO);
            
            projectTask!.Project.Should().BeNull();
            projectTask.Tags.Should().BeNull();
        }

        [Fact(DisplayName = "Given ProjectTask with Project and Tag, " +
                            "When create ProjectTaskDTO, " +
                            "Then ProjectTaskDTO with Project and Tag is returned.")]
        public void CreateProjectTaskDTOWithValidProjectAndTag() {
            Project _project = new() {
                ID = _projectTaskDTO.Project!.ID.ToString()!,
                Name = _projectTaskDTO.Project.Name,
                Color = _projectTaskDTO.Project.Color
            };
            HashSet<Tag> tags = _projectTaskDTO.Tags!.Select(tagDTO => new Tag {
                ID = tagDTO!.ID.ToString()!,
                Name = tagDTO.Name,
                Color = tagDTO.Color
            }).ToHashSet();
            
            ProjectTask projectTask = new() {
                ID = _projectTaskDTO.ID.ToString()!,
                Name = _projectTaskDTO.Name,
                TimeEstimated = _projectTaskDTO.TimeEstimated,
                TimeRecords = _projectTaskDTO.TimeRecords,
                Project = _project,
                Tags = tags
            };
            
            ProjectTaskDTO? projectTaskDTO = _projectTaskFactory.CreateDTO(projectTask);
            
            projectTaskDTO.Should().NotBeNull();
            projectTaskDTO!.ID.Should().Be(new Guid(projectTask.ID));
            projectTaskDTO.Name.Should().Be(projectTask.Name);
            projectTaskDTO.TimeEstimated.Should().Be(projectTask.TimeEstimated);
            projectTaskDTO.TimeRecords.Should().BeEquivalentTo(projectTask.TimeRecords);
            
            projectTaskDTO.Project.Should().NotBeNull();
            projectTaskDTO.Project!.Should().BeEquivalentTo(_projectFactory.CreateDTO(_project));
            
            projectTaskDTO.Tags.Should().NotBeNull();
            projectTaskDTO.Tags!.Should().HaveCount(projectTask.Tags.Count);
            foreach (Tag tag in projectTask.Tags) {
                projectTaskDTO.Tags.Should().ContainEquivalentOf(_tagFactory.CreateDTO(tag));
            }
        }
    }
}