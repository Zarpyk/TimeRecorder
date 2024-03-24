using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort {
    public interface IFindProjectTaskOutPort {
        public Task<ProjectTask?> FindTask(string id);
    }
}