using TimeRecorderAPI.DTO;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort {
    public interface IFindProjectTaskInPort {
        public Task<ProjectTaskDTO?> FindTask(string id);
    }
}