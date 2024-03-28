﻿using TimeRecorderDomain.Shared;

namespace TimeRecorderDomain.DTO {
    public record ProjectTaskDTO(Guid? ID) {
        public Guid? ID { get; internal set; } = ID;
        public string? Name { get; set; }
        public TimeSpan TimeEstimated { get; set; } = TimeSpan.Zero;
        public HashSet<TimeRecord>? TimeRecords { get; set; }
        public Guid? ProjectID { get; set; }
        public HashSet<Guid>? TagIDs { get; set; }
    }
}