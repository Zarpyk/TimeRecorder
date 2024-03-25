﻿using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IAddProjectTaskOutPort))]
    public class AddProjectTaskOutAdapter(IDataBaseManager db) : IAddProjectTaskOutPort {
        public async Task<bool> AddTask(ProjectTask projectTask) {
            await db.Insert(projectTask);
            return true;
        }
    }
}