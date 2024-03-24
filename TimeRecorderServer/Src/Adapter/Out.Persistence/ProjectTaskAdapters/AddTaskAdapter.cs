using TimeRecorderDomain.Models;
using TimeRecorderServer.Application.Port.Out.Persistence.ProjectTask;
using TimeRecorderServer.Configuration.Adapter;
using TimeRecorderServer.DB;
using TimeRecorderServer.DTO;
using TimeRecorderServer.Factory;

namespace TimeRecorderServer.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IAddTaskPort))]
    public class AddTaskAdapter(IDataBaseManager db, ProjectTaskFactory taskFactory) : IAddTaskPort {
        public bool AddTask(ProjectTaskDTO projectTaskDTO) {
            ProjectTask? projectTask = taskFactory.CreateTask(projectTaskDTO);
            if (projectTask == null) return false;
            db.Insert(projectTask);
            return true;
        }
    }
}