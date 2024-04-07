using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort {
    public interface IGenericModifyOutPort<T> where T : IDTO {
        public Task<T?> Replace(string id, T dto);
    }
}