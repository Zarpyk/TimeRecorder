using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort {
    public interface IGenericFindOutPort<T> where T : IDTO {
        public Task<T?> Find(string id);
        Task<List<T>> FindAll();
    }
}