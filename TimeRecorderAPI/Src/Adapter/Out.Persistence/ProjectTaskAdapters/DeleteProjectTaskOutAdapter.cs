using TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IDeleteProjectTaskOutPort))]
    public class DeleteProjectTaskOutAdapter(IDataBaseManager db) :
        GenericDeleteOutAdapter<ProjectTask, ProjectTaskDTO>(db), IDeleteProjectTaskOutPort;
}