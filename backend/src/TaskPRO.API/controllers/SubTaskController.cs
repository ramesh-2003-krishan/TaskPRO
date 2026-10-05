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

        [HttpPut("{projectId}/{taskItemId}")]
        [Authorize]
        public async Task<IActionResult> UpdateSubTask([FromBody] UpdateSubTaskRequest request, Guid projectId, int taskItemId)
        {
            var updatedSubTask = await _subTaskService.UpdateSubTaskAsync(request, projectId, taskItemId);
            return Ok(updatedSubTask);
        }

        [HttpGet("{projectId}/{taskItemId}")]
        [Authorize]
        public async Task<IActionResult> GetSubTask(Guid projectId, int taskItemId)
        {
            var subTask = await _subTaskService.SubTaskResponseAsync(taskItemId, projectId);
            if (subTask is null)
            {
                return NotFound();
            }
            return Ok(subTask);
        }
    }

    
}
