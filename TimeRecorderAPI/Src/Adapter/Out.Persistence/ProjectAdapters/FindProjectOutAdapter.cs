using TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Factory;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectAdapters {
    [PortAdapter(typeof(IFindProjectOutPort))]
    public class FindProjectOutAdapter(
        IDataBaseManager db,
        ProjectFactory factory
    ) : GenericFindOutAdapter<Project, ProjectDTO, ProjectFactory>(db, factory), IFindProjectOutPort;
}