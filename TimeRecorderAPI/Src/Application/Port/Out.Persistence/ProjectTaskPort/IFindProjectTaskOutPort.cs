using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort {
    public interface IFindProjectTaskOutPort {
        public Task<ProjectTaskController?> FindTask(string id);
    }
}