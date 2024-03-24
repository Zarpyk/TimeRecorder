using Microsoft.AspNetCore.Mvc;
using TimeRecorderServer.Application.Port.Out.Persistence.ProjectTask;
using TimeRecorderServer.DTO;

namespace TimeRecorderServer.Adapter.In.RestController {
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController(IAddTaskPort addTaskPort, IFindTaskPort findTaskPort) : ControllerBase {
        [HttpGet("{id}")]
        public ActionResult<ProjectTaskDTO> Get(string id) {
            ProjectTaskDTO? projectTaskDTO = findTaskPort.FindTask(id);
            if (projectTaskDTO == null) return NotFound();
            return Ok(projectTaskDTO);
        }

        [HttpPost]
        public ActionResult<string> Post(ProjectTaskDTO projectTaskDTO) {
            bool addTask = addTaskPort.AddTask(projectTaskDTO);
            return addTask ? Ok() : BadRequest();
        }
    }
}