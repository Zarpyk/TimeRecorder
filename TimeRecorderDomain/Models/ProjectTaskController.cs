using MongoDB.Bson.Serialization.Attributes;

namespace TimeRecorderDomain.Models {
    public class ProjectTaskController : IDBObject {
        [BsonId]
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public TimeSpan TimeEstimated { get; set; } = TimeSpan.Zero;
        public HashSet<TimeRecord>? TimeRecords { get; set; }
        public Project? Project { get; set; }
        public HashSet<Tag>? Tags { get; set; }
    }

    public record TimeRecord(DateTime StartTime, DateTime EndTime) {
        public DateTime StartTime { get; set; } = StartTime;
        public DateTime EndTime { get; set; } = EndTime;
    }
}