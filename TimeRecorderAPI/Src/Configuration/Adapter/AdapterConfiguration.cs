using System.Reflection;

namespace TimeRecorderAPI.Configuration.Adapter {
    public static class AdapterConfiguration {
        public static void AddAdapters(this IServiceCollection service) {
            // Find all classes with the PortAdapterAttribute and register them
            IEnumerable<Type> adapterTypes = Assembly.GetExecutingAssembly().GetTypes()
                                                     .Where(t => t.GetCustomAttributes<PortAdapterAttribute>().Any());
            foreach (Type adapterType in adapterTypes) {
                PortAdapterAttribute attribute = adapterType.GetCustomAttribute<PortAdapterAttribute>()!;
                service.AddScoped(attribute.PortType, adapterType);
            }
        }
    }
}