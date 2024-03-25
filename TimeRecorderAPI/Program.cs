using Microsoft.AspNetCore.Mvc;
using TimeRecorderAPI.Configuration;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.Exceptions;
using TimeRecorderAPI.Exceptions.Responses;

namespace TimeRecorderAPI {
    internal static class Program {
        public const string EnvPrefix = "TIMERECORDER_";

        public static void Main(string[] args) {
            WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

            builder.Logging.AddSimpleLogger();
            builder.Services.AddDatabase();
            builder.Services.AddFactories();
            builder.Services.AddAdapters();

            builder.Services.AddExceptions();
            builder.Services.AddControllers();

            builder.Services.AddSwagger();
            builder.Services.AddFluentValidation();

            WebApplication app = builder.Build();

            app.UseExceptionHandler(_ => { });
            app.UseHsts();

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "TimeRecorder API V1"); });
                // app.UseDeveloperExceptionPage();
            }

            app.MapControllers();

            app.Run();
        }
    }
}