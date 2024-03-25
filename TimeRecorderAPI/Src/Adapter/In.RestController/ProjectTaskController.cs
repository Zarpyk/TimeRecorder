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
        public async Task<IResult> Get(string id) {
            ProjectTaskDTO? projectTaskDTO = await findProjectTaskOutPort.FindTask(id);
            return projectTaskDTO == null ? Results.NotFound() : Results.Ok(projectTaskDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationExceptionResponse))]
        public async Task<IResult> Post(ProjectTaskDTO projectTaskDTO) {
            await validator.ValidateData(projectTaskDTO);

            bool addTask = await addProjectTaskOutPort.AddTask(projectTaskDTO);
            return addTask ? Results.Created() : Results.BadRequest();
        }
    }
}