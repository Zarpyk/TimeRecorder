using FluentValidation;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Validators {
    public class TagDTOValidator : AbstractValidator<TagDTO> {
        public TagDTOValidator() {
            RuleFor(x => x.Color).Matches("^#([A-Fa-f0-9]{6})$");
        }
    }
}