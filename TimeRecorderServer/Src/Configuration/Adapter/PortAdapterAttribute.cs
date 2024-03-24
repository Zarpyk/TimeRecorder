namespace TimeRecorderServer.Configuration.Adapter {
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PortAdapterAttribute(Type portType) : Attribute {
        public Type PortType { get; } = portType;
    }
}