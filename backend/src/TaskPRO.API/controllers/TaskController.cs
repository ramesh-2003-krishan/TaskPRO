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
    }
}