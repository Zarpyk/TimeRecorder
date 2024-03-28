using TimeRecorderDomain.DTO;
using TimeRecorderDomain.Shared;

namespace TimeRecorderAPITests.Fixtures {
    public class ProjectTaskDTOFixture {
        private readonly ProjectTaskDTO _projectTaskDTO;
        public readonly ProjectDTOFixture ProjectDTOFixture = new();
        public readonly TagDTOFixture TagDTOFixture = new();

        public ProjectTaskDTOFixture() {
            DateTime dateTime = new(2024, 1, 1, 12,
                                    0, 0);
            _projectTaskDTO = new ProjectTaskDTO {
                ID = Guid.NewGuid(),
                Name = "Task 1",
                TimeEstimated = TimeSpan.FromHours(1),
                TimeRecords = new HashSet<TimeRecord> { new(dateTime.AddHours(-1), dateTime) },
                Project = ProjectDTOFixture.Get(),
                Tags = new HashSet<TagDTO?> { TagDTOFixture.Get() }
            };
        }

        public ProjectTaskDTO Get() {
            return _projectTaskDTO;
        }

        public string ID => _projectTaskDTO.ID.ToString()!;
    }
}