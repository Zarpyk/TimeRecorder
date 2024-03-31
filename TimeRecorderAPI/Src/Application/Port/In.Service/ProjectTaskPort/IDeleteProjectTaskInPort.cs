﻿using TimeRecorderAPI.Application.Port.In.Service.GenericPort;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort {
    public interface IDeleteProjectTaskInPort : IGenericDeleteInPort<ProjectTaskDTO>;
}