using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderAPI.DB;
using TimeRecorderDomain;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters {
    /// <summary>
    /// Generic adapter for deleting objects from the database.
    /// </summary>
    /// <param name="db">Database manager</param>
    /// <typeparam name="T">Database Object</typeparam>
    /// <typeparam name="U">DTO Object</typeparam>
    public class GenericDeleteOutAdapter<T, U>(IDataBaseManager db) : IGenericDeleteOutPort<U>
        where T : IDBObject, new()
        where U : IDTO {
        public async Task<bool> Delete(string id) {
            return await db.Delete<T>(id);
        }
    }
}