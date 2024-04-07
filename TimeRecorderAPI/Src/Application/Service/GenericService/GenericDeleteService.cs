using TimeRecorderAPI.Application.Port.In.Service.GenericPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.GenericService {
    public class GenericDeleteService<T>(IGenericDeleteOutPort<T> outPort) : IGenericDeleteInPort<T> where T : IDTO {
        public Task<bool> Delete(string id) {
            return outPort.Delete(id);
        }
    }
}