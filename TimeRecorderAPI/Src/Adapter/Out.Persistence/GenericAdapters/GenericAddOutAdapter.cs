using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Factory;
using TimeRecorderDomain;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters {
    /// <summary>
    /// Generic adapter for adding objects to the database.
    /// </summary>
    /// <param name="db">Database manager</param>
    /// <param name="factory">Factory to transform DTOs</param>
    /// <typeparam name="T">Database Object</typeparam>
    /// <typeparam name="U">DTO Object</typeparam>
    /// <typeparam name="V">Factory Type</typeparam>
    public class GenericAddOutAdapter<T, U, V>(
        IDataBaseManager db,
        V factory
    ) : IGenericAddOutPort<U>
        where T : IDBObject, new()
        where U : IDTO
        where V : IFactory<T, U> {
        public async Task<U> Add(U dto) {
            T projectTask = (await factory.Create(dto))!;
            await db.Insert(projectTask);
            return factory.CreateDTO(projectTask)!;
        }
    }
}