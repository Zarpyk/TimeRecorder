using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.DTO;
using TimeRecorderAPI.Exceptions.Responses;
using TimeRecorderAPI.Extensions;
using ValidationException = TimeRecorderAPI.Exceptions.ValidationException;

namespace TimeRecorderAPI.Adapter.In.RestController {
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController(
        IAddProjectTaskInPort addProjectTaskOutPort,
        IFindProjectTaskInPort findProjectTaskOutPort,
        IValidator<ProjectTaskDTO> validator
    ) : ControllerBase {

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) {
            ProjectTaskDTO? projectTaskDTO = await findProjectTaskOutPort.FindTask(id);
            return projectTaskDTO == null ? NotFound() : Ok(projectTaskDTO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectTaskDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationExceptionResponse))]
        public async Task<IActionResult> Post(ProjectTaskDTO projectTaskDTO) {
            await validator.ValidateData(projectTaskDTO);

            ProjectTaskDTO? addTask = await addProjectTaskOutPort.AddTask(projectTaskDTO);
            if (addTask == null) return Conflict();
            return CreatedAtAction(nameof(Get), new { id = addTask.ID }, addTask);
        }
    }
}