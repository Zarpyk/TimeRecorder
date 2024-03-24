using System.Linq.Expressions;
using TimeRecorderDomain;

namespace TimeRecorderServer.DB {
    // Made for MongoDB, so possibly need refactoring for other DBs 
    public interface IDataBaseManager {
        public T? Find<T>(string id, bool checkStatus = true) where T : IDBObject, new();
        public T? Find<T>(Expression<Func<T, bool>> filter, bool checkStatus = true) where T : IDBObject, new();
        public List<T> Find<T>(string id, int quantity, bool checkStatus = true) where T : IDBObject, new();

        public List<T> Find<T>(Expression<Func<T, bool>> filter, int quantity, bool checkStatus = true)
            where T : IDBObject, new();

        public List<T> FindAll<T>(bool checkStatus = true) where T : IDBObject, new();
        public T? FindLast<T>() where T : IDBObject, new();

        public void Insert<T>(T obj) where T : IDBObject, new();
        public void InsertAll<T>(IEnumerable<T> list) where T : IDBObject, new();

        public void Replace<T>(T obj, bool checkStatus = true) where T : IDBObject, new();
        public void ReplaceAll<T>(List<T> list, bool checkStatus = true) where T : IDBObject, new();

        public void Delete<T>(string id) where T : IDBObject, new();
        public void Delete<T>(T obj) where T : IDBObject, new();
        public void Delete<T>(Predicate<T> predicate) where T : IDBObject, new();
        public void Delete<T>(List<T> toDeleteProducts) where T : IDBObject, new();
    }
}