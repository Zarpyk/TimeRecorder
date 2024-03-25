using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using TimeRecorderAPI.DTO;
using TimeRecorderAPI.Exceptions;

namespace TimeRecorderAPI.Configuration {
    public static class FluentValidationConfiguration {
        public static void AddFluentValidation(this IServiceCollection services) {
            services.AddValidatorsFromAssemblyContaining<ProjectTaskDTOValidator>();
            services.AddFluentValidationRulesToSwagger();
            
            services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = InvalidModelStateHandler.Response;
            });
        }
    }
}