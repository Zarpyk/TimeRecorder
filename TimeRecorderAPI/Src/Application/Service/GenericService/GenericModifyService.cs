using TimeRecorderAPI.Application.Port.In.Service.GenericPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.GenericService {
    public class GenericModifyService<T>(IGenericModifyOutPort<T> outPort) : IGenericModifyInPort<T> where T : IDTO{
        public Task<T?> Replace(string id, T dto) {
            return outPort.Replace(id, dto);
        }
    }
}