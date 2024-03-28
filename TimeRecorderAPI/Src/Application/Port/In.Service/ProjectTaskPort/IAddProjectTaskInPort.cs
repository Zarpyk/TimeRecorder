using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort {
    public interface IAddProjectTaskInPort {
        public Task<ProjectTaskDTO> AddTask(ProjectTaskDTO projectTaskDTO);
    }
}