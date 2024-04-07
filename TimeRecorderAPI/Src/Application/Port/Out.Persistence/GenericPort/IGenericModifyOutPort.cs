using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort {
    public interface IGenericModifyOutPort<T> {
        public Task<T?> Replace(string id, T dto);
    }
}