using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort {
    public interface IAddProjectTaskOutPort {
        public Task<ProjectTaskDTO> AddTask(ProjectTaskDTO projectTaskDTO);
    }
}