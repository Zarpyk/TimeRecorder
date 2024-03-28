using TimeRecorderDomain.DTO;
using TimeRecorderDomain.Shared;

namespace TimeRecorderAPITests.Fixtures {
    public class ProjectTaskDTOFixture {
        private readonly ProjectTaskDTO _projectTaskDTO;

        public ProjectTaskDTOFixture() {
            DateTime dateTime = new(2024, 1, 1, 12,
                                    0, 0);
            _projectTaskDTO = new ProjectTaskDTO(Guid.NewGuid()) {
                Name = "Task 1",
                TimeEstimated = TimeSpan.FromHours(1),
                TimeRecords = new HashSet<TimeRecord> { new(dateTime.AddHours(-1), dateTime) },
                ProjectID = Guid.NewGuid(),
                TagIDs = new HashSet<Guid> { Guid.NewGuid() }
            };
        }

        public ProjectTaskDTO Get() {
            return _projectTaskDTO;
        }
        
        public string ID => _projectTaskDTO.ID.ToString()!;
    }
}