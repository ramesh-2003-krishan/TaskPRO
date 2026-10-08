using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using TaskPRO.Application.features.Task.DTOs;
using TaskPRO.Application.features.Task.Interfaces;
using TaskPRO.Application.features.Task.Services;
using TaskPRO.Application.interfaces;

namespace TaskPRO.API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ICurrentUserService _currentUserService;
        public TaskController(ITaskService taskService, ICurrentUserService currentUserService)
        {
            _taskService = taskService;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateTaskRequest>> CreateTask ([FromBody]CreateTaskRequest request)
        {
            var currentUserId = _currentUserService.UserId;
            if(currentUserId == null)
            {
                throw new Exception ("you are not allowed to add a task");
            }
            return CreatedAtAction(nameof(CreateTask), new{id = request.Title},request);
        }

        [HttpGet("{projectId:guid}/tasks/{taskId:int}")]
        public async Task<ActionResult<TaskResponse>> GetTask(Guid projectId, int taskId)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                throw new Exception("you are not allowed to view a task");
            }

            var task = await _taskService.TaskResponseAsync(taskId, projectId);
            return Ok(task);
        }

        [HttpPut("{projectId}")]
        public async Task<ActionResult<UpdateTaskRequest>> UpdateTask (Guid projectId, [FromBody]UpdateTaskRequest request)
        {
            var currentUserId = _currentUserService.UserId;
            if(currentUserId == null)
            {
                throw new Exception ("you are not allowed to update task");
            }

            var updatedTask = await _taskService.UpdateTaskRequestAsync(request, projectId);
            return Ok(updatedTask);
        }

        [HttpPut("{projectId:guid}/tasks/{taskId:int}/assignment")]
        public async Task<ActionResult<AssignTaskRequest>> AssignTask(Guid projectId, int taskId, [FromBody] AssignTaskRequest request)
        {
            var currentUserId = _currentUserService.UserId;
            if(currentUserId == null)
            {
                throw new Exception ("you are not allowed to assign a task");
            }

            var assignedTask = await _taskService.AssignTaskRequestAsync(request, projectId, taskId);
            return Ok(assignedTask);

        }

        [HttpDelete("{projectId:guid}/tasks/{taskId:int}")]
        public async Task<ActionResult<DeleteTaskRequest>> DeleteTask(Guid projectId, int taskId)
        {
            var currentUserId = _currentUserService.UserId;
            if(currentUserId == null)
            {
                throw new Exception ("you are not allowed to delete a task");
            }

            var deleteTaskRequest = new DeleteTaskRequest { Id = taskId };
            var deletedTask = await _taskService.DeleteTaskRequestAsync(deleteTaskRequest, projectId, taskId);
            return Ok(deletedTask);
        }

        [HttpGet("{projectId:guid}/tasks/sort")]
        public async Task<ActionResult<IEnumerable<TaskResponse>>> SortTasks(Guid projectId, [FromQuery] SortTaskRequest request)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                throw new Exception("you are not allowed to sort tasks");
            }

            var sortedTasks = await _taskService.SortTaskRequestAsync(request, projectId);
            return Ok(sortedTasks);
        }
    }
}