using Microsoft.AspNetCore.Mvc;
using TimeRecorderServer.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderServer.DTO;

namespace TimeRecorderServer.Adapter.In.RestController {
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController(
        IAddProjectTaskInPort addProjectTaskOutPort,
        IFindProjectTaskInPort findProjectTaskOutPort
    ) : ControllerBase {

        [HttpGet("{id}")]
        public ActionResult<ProjectTaskDTO> Get(string id) {
            ProjectTaskDTO? projectTaskDTO = findProjectTaskOutPort.FindTask(id);
            if (projectTaskDTO == null) return NotFound();
            return Ok(projectTaskDTO);
        }

        [HttpPost]
        public ActionResult<string> Post(ProjectTaskDTO projectTaskDTO) {
            bool addTask = addProjectTaskOutPort.AddTask(projectTaskDTO);
            return addTask ? Ok() : BadRequest();
        }
    }
}