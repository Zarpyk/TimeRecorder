using TimeRecorderAPI.Application.Port.In.Service.TagPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.TagPort;
using TimeRecorderAPI.Application.Service.GenericService;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.TagService {
    [PortAdapter(typeof(IDeleteTagInPort))]
    public class DeleteTagService(IDeleteTagOutPort outPort)
        : GenericDeleteService<TagDTO>(outPort), IDeleteTagInPort;
}