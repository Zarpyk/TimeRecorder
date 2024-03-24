using TimeRecorderAPI.Factory;

namespace TimeRecorderAPI.Configuration {
    public static class FactoryConfiguration {
        public static void AddFactories(this IServiceCollection services) {
            services.AddSingleton<ProjectTaskFactory>();
        }
    }
}