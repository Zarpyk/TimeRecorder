using MongoDB.Bson.Serialization.Attributes;
using TimeRecorderDomain;
using TimeRecorderDomain.Shared;

namespace TimeRecorderAPI.Models {
    public class ProjectTask : IDBObject {
        [BsonId]
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public TimeSpan TimeEstimated { get; set; }
        public HashSet<TimeRecord>? TimeRecords { get; set; }
        public Project? Project { get; set; }
        public HashSet<Tag>? Tags { get; set; }
    }
}