using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort {
    public interface IGenericAddOutPort<T> {
        public Task<T> Add(T dto);
    }
}