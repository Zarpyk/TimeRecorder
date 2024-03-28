using System.Linq.Expressions;
using TimeRecorderDomain;

namespace TimeRecorderAPI.DB {
    // Made for MongoDB, so possibly need refactoring for other DBs 
    public interface IDataBaseManager {
        public Task<T?> Find<T>(string id) where T : IDBObject, new();
        public Task<T?> Find<T>(Expression<Func<T, bool>> filter) where T : IDBObject, new();
        public Task<List<T>> Find<T>(string id, int quantity) where T : IDBObject, new();

        public Task<List<T>> Find<T>(Expression<Func<T, bool>> filter, int quantity)
            where T : IDBObject, new();

        public Task<List<T>> FindAll<T>() where T : IDBObject, new();
        public Task<T?> FindLast<T>() where T : IDBObject, new();

        public Task Insert<T>(T obj) where T : IDBObject, new();
        public Task InsertAll<T>(IEnumerable<T> list) where T : IDBObject, new();

        public Task<bool> Replace<T>(T obj) where T : IDBObject, new();
        public Task ReplaceAll<T>(List<T> list) where T : IDBObject, new();

        public Task Delete<T>(string id) where T : IDBObject, new();
        public Task Delete<T>(T obj) where T : IDBObject, new();
        public Task Delete<T>(Predicate<T> predicate) where T : IDBObject, new();
        public Task Delete<T>(List<T> toDeleteProducts) where T : IDBObject, new();
    }
}