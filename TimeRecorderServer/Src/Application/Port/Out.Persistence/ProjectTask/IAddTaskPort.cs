using TimeRecorderServer.DTO;

namespace TimeRecorderServer.Application.Port.Out.Persistence.ProjectTask {
    public interface IAddTaskPort {
        public bool AddTask(ProjectTaskDTO projectTaskDTO);
    }
}