namespace TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort {
    public interface IGenericDeleteOutPort<T> {
        public Task<bool> Delete(string id);
    }
}