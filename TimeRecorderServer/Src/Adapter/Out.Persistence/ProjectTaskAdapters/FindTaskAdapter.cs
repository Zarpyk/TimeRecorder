using TimeRecorderDomain.Models;
using TimeRecorderServer.Application.Port.Out.Persistence.ProjectTask;
using TimeRecorderServer.Configuration.Adapter;
using TimeRecorderServer.DB;
using TimeRecorderServer.DTO;
using TimeRecorderServer.Factory;

namespace TimeRecorderServer.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IFindTaskPort))]
    public class FindTaskAdapter(IDataBaseManager db, ProjectTaskFactory taskFactory) : IFindTaskPort {
        public ProjectTaskDTO? FindTask(string id) {
            ProjectTask? projectTask = db.Find<ProjectTask>(id);
            return taskFactory.CreateTaskDTO(projectTask);
        }
    }
}