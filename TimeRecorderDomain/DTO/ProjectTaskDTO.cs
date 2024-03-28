using Swashbuckle.AspNetCore.Annotations;
using TimeRecorderDomain.Shared;

namespace TimeRecorderDomain.DTO {
    public record ProjectTaskDTO() {
        [SwaggerSchema(ReadOnly = true)]
        public Guid? ID { get; set; }
        public string? Name { get; set; }
        public TimeSpan TimeEstimated { get; set; } = TimeSpan.Zero;
        public HashSet<TimeRecord>? TimeRecords { get; set; }
        public ProjectDTO? Project { get; set; }
        public HashSet<TagDTO?>? Tags { get; set; }
    }
}