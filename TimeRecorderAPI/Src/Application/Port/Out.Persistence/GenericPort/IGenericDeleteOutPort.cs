using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort {
    public interface IGenericDeleteOutPort<T> where T : IDTO {
        public Task<bool> Delete(string id);
    }
}