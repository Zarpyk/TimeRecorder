using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.In.Service.GenericPort {
    public interface IGenericDeleteInPort<T> where T : IDTO {
        public Task<bool> Delete(string id);
    }
}