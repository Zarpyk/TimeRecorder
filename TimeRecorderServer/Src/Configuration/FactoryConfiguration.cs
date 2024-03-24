using TimeRecorderServer.Factory;

namespace TimeRecorderServer.Configuration {
    public static class FactoryConfiguration {
        public static void AddFactories(this IServiceCollection services) {
            services.AddSingleton<ProjectTaskFactory>();
        }
    }
}