using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.In.Service.GenericPort {
    public interface IGenericModifyInPort<T> where T : IDTO {
        public Task<T?> Replace(string id, T dto);
    }
}