using TimeRecorderDomain;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Factory {
    public interface IFactory<T, U> where T : IDBObject where U : IDTO {
        public Task<T?> Create(U? dto);
        public U? CreateDTO(T? entity);
    }
}