﻿using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.ProjectPort {
    public interface IDeleteProjectOutPort : IGenericDeleteOutPort<ProjectDTO>;
}