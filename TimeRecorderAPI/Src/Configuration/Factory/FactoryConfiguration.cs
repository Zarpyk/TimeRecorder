using System.Reflection;
using TimeRecorderAPI.Configuration.Factory;

namespace TimeRecorderAPI.Configuration {
    public static class FactoryConfiguration {
        public static void AddFactories(this IServiceCollection service) {
            // Find all classes with the PortAdapterAttribute and register them
            IEnumerable<Type> factories = Assembly.GetExecutingAssembly().GetTypes()
                                                     .Where(t => t.GetCustomAttributes<FactoryAttribute>().Any());
            foreach (Type factory in factories) {
                service.AddSingleton(factory);
            }
        }
    }
}