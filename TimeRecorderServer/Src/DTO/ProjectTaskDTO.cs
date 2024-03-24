using TimeRecorderDomain.Models;

namespace TimeRecorderServer.DTO {
    public record ProjectTaskDTO {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TimeSpan TimeEstimated { get; set; } = TimeSpan.Zero;
        public HashSet<TimeRecord>? TimeRecords { get; set; }
        public Guid? ProjectID { get; set; }
        public HashSet<Guid>? TagIDs { get; set; }
    }
}