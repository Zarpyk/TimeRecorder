﻿using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.DB;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    public class DeleteProjectTaskOutAdapter(IDataBaseManager db) : IDeleteProjectTaskOutPort {

        public async Task<bool> DeleteTask(string id) {
            return await db.Delete<ProjectTask>(id);
        }
    }
}