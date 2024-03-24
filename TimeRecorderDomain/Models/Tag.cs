using MongoDB.Bson.Serialization.Attributes;

namespace TimeRecorderDomain.Models {
    public class Tag : IDBObject {
        [BsonId]
        public string ID { get; set; } = Guid.NewGuid().ToString();
    }
}