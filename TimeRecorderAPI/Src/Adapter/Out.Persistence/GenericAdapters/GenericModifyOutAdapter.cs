using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Factory;
using TimeRecorderDomain;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters {
    /// <summary>
    /// Generic adapter for modifying objects in the database.
    /// </summary>
    /// <param name="db">Database manager</param>
    /// <param name="factory">Factory to transform DTOs</param>
    /// <typeparam name="T">Database Object</typeparam>
    /// <typeparam name="U">DTO Object</typeparam>
    /// <typeparam name="V">Factory Type</typeparam>
    public class GenericModifyOutAdapter<T, U, V>(
        IDataBaseManager db,
        V factory
    ) : IGenericModifyOutPort<U>
        where T : IDBObject, new()
        where U : IDTO
        where V : IFactory<T, U> {
        public async Task<U?> Replace(string id, U projectTaskDTO) {
            T? projectTask = await factory.Create(projectTaskDTO);
            projectTask!.ID = id;
            bool replace = await db.Replace(projectTask);
            return !replace ? default : factory.CreateDTO(projectTask);
        }
    }
}