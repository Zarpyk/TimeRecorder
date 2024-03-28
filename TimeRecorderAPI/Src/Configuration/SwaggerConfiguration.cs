using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace TimeRecorderAPI.Configuration {
    public static class SwaggerConfiguration {
        public static void AddSwagger(this IServiceCollection services) {
            // Add Swagger
            services.AddSwaggerGen(options => {
                // Allow nullable reference types
                options.SupportNonNullableReferenceTypes();
                // Map correctly TimeSpan on Swagger
                options.MapType(typeof(TimeSpan), () => new OpenApiSchema {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
                // Enable Swashbuckle.AspNetCore.Annotations
                options.EnableAnnotations();
            });
        }
    }
}