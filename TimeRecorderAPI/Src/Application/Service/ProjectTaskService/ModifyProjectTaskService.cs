using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Application.Service.GenericService;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.ProjectTaskService {
    [PortAdapter(typeof(IModifyProjectTaskInPort))]
    public class ModifyProjectTaskService(IModifyProjectTaskOutPort outPort)
        : GenericModifyService<ProjectTaskDTO>(outPort), IModifyProjectTaskInPort;
}