using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Factory;
using TimeRecorderDomain;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters {
    /// <summary>
    /// Generic adapter for finding objects in the database.
    /// </summary>
    /// <param name="db">Database manager</param>
    /// <param name="factory">Factory to transform DTOs</param>
    /// <typeparam name="T">Database Object</typeparam>
    /// <typeparam name="U">DTO Object</typeparam>
    /// <typeparam name="V">Factory Type</typeparam>
    public class GenericFindOutAdapter<T, U, V>(
        IDataBaseManager db,
        V factory
    ) : IGenericFindOutPort<U>
        where T : IDBObject, new()
        where U : IDTO
        where V : IFactory<T, U> {
        public async Task<U?> Find(string id) {
            T? projectTask = await db.Find<T>(id);

            return factory.CreateDTO(projectTask);
        }

        public async Task<List<U>> FindAll() {
            List<T> projectTasks = await db.FindAll<T>();
            return projectTasks.Select(projectTask => factory.CreateDTO(projectTask)!).ToList();
        }
    }
}