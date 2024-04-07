using TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Factory;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IModifyProjectTaskOutPort))]
    public class ModifyProjectTaskOutAdapter(
        IDataBaseManager db,
        ProjectTaskFactory factory
    ) : GenericModifyOutAdapter<ProjectTask, ProjectTaskDTO, ProjectTaskFactory>(db, factory), IModifyProjectTaskOutPort;
}