using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Application.Service.GenericService;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.ProjectTaskService {
    [PortAdapter(typeof(IFindProjectTaskInPort))]
    public class FindProjectTaskService(IFindProjectTaskOutPort outPort)
        : GenericFindService<ProjectTaskDTO>(outPort), IFindProjectTaskInPort;
}