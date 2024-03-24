using TimeRecorderServer.DTO;

namespace TimeRecorderServer.Application.Port.In.Service.ProjectTaskPort {
    public interface IAddProjectTaskInPort {
        public bool AddTask(ProjectTaskDTO projectTaskDTO);
    }
}