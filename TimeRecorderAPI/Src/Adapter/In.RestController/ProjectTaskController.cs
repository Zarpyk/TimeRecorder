﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Exceptions.Responses;
using TimeRecorderAPI.Extensions;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.In.RestController {
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController(
        IFindProjectTaskInPort findProjectTaskOutPort,
        IAddProjectTaskInPort addProjectTaskOutPort,
        IModifyProjectTaskInPort modifyProjectTaskOutPort,
        IDeleteProjectTaskInPort deleteProjectTaskOutPort,
        IValidator<ProjectTaskDTO> validator
    ) : ControllerBase {

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectTaskDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id) {
            ProjectTaskDTO? projectTaskDTO = await findProjectTaskOutPort.FindTask(id);
            return projectTaskDTO == null ? NotFound() : Ok(projectTaskDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectTaskDTO))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationExceptionResponse))]
        public async Task<IActionResult> Post(ProjectTaskDTO projectTaskDTO) {
            await validator.ValidateData(projectTaskDTO);

            ProjectTaskDTO? addTask = await addProjectTaskOutPort.AddTask(projectTaskDTO);
            if (addTask == null) return Conflict();
            return CreatedAtAction(nameof(Get), new { id = addTask.ID }, addTask);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectTaskDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationExceptionResponse))]
        public async Task<IActionResult> Put(string id, ProjectTaskDTO projectTaskDTO) {
            await validator.ValidateData(projectTaskDTO);

            ProjectTaskDTO? task = await modifyProjectTaskOutPort.ReplaceTask(id, projectTaskDTO);

            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id) {
            bool deleteTask = await deleteProjectTaskOutPort.DeleteTask(id);
            
            if (!deleteTask) return NotFound();
            return Ok();
        }
    }
}