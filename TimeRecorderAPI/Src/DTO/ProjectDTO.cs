﻿using FluentValidation;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.DTO {
    public class ProjectDTOValidator : AbstractValidator<ProjectDTO> {
        public ProjectDTOValidator() {
            RuleFor(x => x.Color).Matches("^#([A-Fa-f0-9]{6})$");
        }
    }
}