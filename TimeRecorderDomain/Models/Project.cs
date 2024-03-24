using MongoDB.Bson.Serialization.Attributes;

namespace TimeRecorderDomain.Models {
    public class Project : IDBObject {
        [BsonId]
        public string ID { get; set; } = Guid.NewGuid().ToString();
    }
}