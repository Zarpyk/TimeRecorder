using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using TimeRecorderAPI.Configuration;
using TimeRecorderAPI.Configuration.Adapter;

namespace TimeRecorderAPI {
    internal static class Program {
        public const string EnvPrefix = "TIMERECORDER_";

        public static void Main(string[] args) {
            WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

            builder.Logging.AddSimpleLogger();
            builder.Services.AddDatabase();
            builder.Services.AddFactories();
            builder.Services.AddAdapters();

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(options => {
                // Allow nullable reference types
                options.SupportNonNullableReferenceTypes();
                // Map correctly TimeSpan on Swagger
                options.MapType(typeof(TimeSpan), () => new OpenApiSchema {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "TimeRecorder API V1"); });
                app.UseDeveloperExceptionPage();
            }

            app.MapControllers();

            app.Run();
        }
    }
}