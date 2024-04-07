using TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectAdapters {
    [PortAdapter(typeof(IDeleteProjectOutPort))]
    public class DeleteProjectOutAdapter(IDataBaseManager db) :
        GenericDeleteOutAdapter<Project, ProjectDTO>(db), IDeleteProjectOutPort;
}