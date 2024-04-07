using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort {
    public interface IGenericFindOutPort<T> {
        public Task<T?> Find(string id);
    }
}