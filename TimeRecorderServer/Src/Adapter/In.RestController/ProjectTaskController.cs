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
        public async Task<ActionResult<ProjectTaskDTO>> Get(string id) {
            ProjectTaskDTO? projectTaskDTO = await findProjectTaskOutPort.FindTask(id);
            if (projectTaskDTO == null) return NotFound();
            return Ok(projectTaskDTO);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(ProjectTaskDTO projectTaskDTO) {
            bool addTask = await addProjectTaskOutPort.AddTask(projectTaskDTO);
            return addTask ? Ok() : BadRequest();
        }
    }
}