namespace TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort {
    public interface IDeleteProjectTaskInPort {
        public Task DeleteTask(string id);
    }
}