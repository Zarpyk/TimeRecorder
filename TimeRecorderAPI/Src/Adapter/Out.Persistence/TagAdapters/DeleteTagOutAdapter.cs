using TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters;
using TimeRecorderAPI.Application.Port.Out.Persistence.TagPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.TagAdapters {
    [PortAdapter(typeof(IDeleteTagOutPort))]
    public class DeleteTagOutAdapter(IDataBaseManager db) :
        GenericDeleteOutAdapter<Tag, TagDTO>(db), IDeleteTagOutPort;
}