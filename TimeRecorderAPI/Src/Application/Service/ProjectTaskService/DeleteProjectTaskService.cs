﻿using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;

namespace TimeRecorderAPI.Application.Service.ProjectTaskService {
    [PortAdapter(typeof(IDeleteProjectTaskInPort))]
    public class DeleteProjectTaskService(IDeleteProjectTaskOutPort outPort) : IDeleteProjectTaskInPort {
        public Task<bool> Delete(string id) {
            return outPort.Delete(id);
        }
    }
}