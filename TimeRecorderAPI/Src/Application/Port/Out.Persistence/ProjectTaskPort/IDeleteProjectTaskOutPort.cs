namespace TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort {
    public interface IDeleteProjectTaskOutPort {
        public Task<bool> DeleteTask(string id);
    }
}