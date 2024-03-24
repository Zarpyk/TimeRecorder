using TimeRecorderServer.DTO;

namespace TimeRecorderServer.Application.Port.Out.Persistence.ProjectTask {
    public interface IFindTaskPort {
        public ProjectTaskDTO? FindTask(string id);
    }
}