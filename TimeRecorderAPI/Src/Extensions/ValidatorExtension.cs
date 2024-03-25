using FluentValidation;
using FluentValidation.Results;
using ValidationException = TimeRecorderAPI.Exceptions.ValidationException;

namespace TimeRecorderAPI.Extensions {
    public static class ValidatorExtension {

        public static async Task ValidateData<T>(this IValidator<T> validator, T data) {
            ValidationResult validationResult = await validator.ValidateAsync(data);
            if (!validationResult.IsValid) {
                throw new ValidationException(validationResult.ToDictionary());
            }
        }
    }
}