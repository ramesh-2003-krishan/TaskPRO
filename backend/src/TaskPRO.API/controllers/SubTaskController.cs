using TaskPRO.Application.features.SubTask.DTOs;
using TaskPRO.Application.features.SubTask.Interfaces;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using SubTaskEntity = TaskPRO.Domain.entities.SubTask;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace TaskPRO.API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubTaskController : ControllerBase
    {
        private readonly ISubTaskService _subTaskService;

        public SubTaskController(ISubTaskService subTaskService)
        {
            _subTaskService = subTaskService;
        }

        [HttpPost("{projectId}/{taskItemId}")]
        [Authorize]
        public async Task<IActionResult> CreateSubTask([FromBody] CreateSubTaskRequest request, Guid projectId, int taskItemId)
        {
            var createdSubTask = await _subTaskService.CreateSubTaskAsync(request, projectId, taskItemId);
            return StatusCode((int)HttpStatusCode.Created, createdSubTask);
        }
    }
}