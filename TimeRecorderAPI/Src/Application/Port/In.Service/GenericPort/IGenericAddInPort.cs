using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.In.Service.GenericPort {
    public interface IGenericAddInPort<T> where T : IDTO {
        public Task<T> Add(T dto);
    }
}