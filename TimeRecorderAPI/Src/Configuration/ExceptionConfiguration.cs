using TimeRecorderAPI.Exceptions;

namespace TimeRecorderAPI.Configuration {
    public static class ExceptionConfiguration {
        public static void AddExceptions(this IServiceCollection services) {
            // Disable "app.UseDeveloperExceptionPage();" to use this
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
        }
    }
}