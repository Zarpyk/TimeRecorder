﻿using TimeRecorderAPI.Application.Port.In.Service.ProjectPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectPort;
using TimeRecorderAPI.Application.Service.GenericService;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.ProjectService {
    [PortAdapter(typeof(IDeleteProjectInPort))]
    public class DeleteProjectService(IDeleteProjectOutPort outPort)
        : GenericDeleteService<ProjectDTO>(outPort), IDeleteProjectInPort;
}