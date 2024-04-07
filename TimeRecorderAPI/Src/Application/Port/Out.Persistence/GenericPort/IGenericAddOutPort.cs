using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort {
    public interface IGenericAddOutPort<T> where T : IDTO {
        public Task<T> Add(T dto);
    }
}