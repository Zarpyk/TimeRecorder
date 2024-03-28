using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort {
    public interface IFindProjectTaskOutPort {
        public Task<ProjectTaskDTO?> FindTask(string id);
    }
}