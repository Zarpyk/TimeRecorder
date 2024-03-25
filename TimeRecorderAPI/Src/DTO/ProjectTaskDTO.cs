﻿using FluentValidation;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.DTO {
    public record ProjectTaskDTO {
        public virtual Guid? ID { get; internal set; }
        public string Name { get; set; } = string.Empty;
        public TimeSpan TimeEstimated { get; set; } = TimeSpan.Zero;
        public HashSet<TimeRecord>? TimeRecords { get; set; }
        public Guid? ProjectID { get; set; }
        public HashSet<Guid>? TagIDs { get; set; }
    }

    public class ProjectTaskDTOValidator : AbstractValidator<ProjectTaskDTO> {
        public ProjectTaskDTOValidator() { }
    }
}