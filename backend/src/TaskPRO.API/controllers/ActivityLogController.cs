using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TaskPRO.Application.features.ActivityLogs.DTOs;
using TaskPRO.Application.common.interfaces;
using TaskPRO.Infrastructure.services;

namespace TaskPRO.API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityLogController : ControllerBase
    {
        private readonly IActivityLogService _activityLogService;

        public ActivityLogController(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        [HttpGet("project/{entityName}/{entityId}")]
        [Authorize]
        public async Task<IActionResult> GetByProject(string entityName, string entityId, CancellationToken ct)
        {
            var activityLogs = await _activityLogService.GetByProjectAsync(entityName, entityId, ct);
            return Ok(activityLogs);
        }

        [HttpGet("task/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetByTask(string userId, CancellationToken ct)
        {
            var activityLogs = await _activityLogService.GetByTaskAsync(userId, ct);
            return Ok(activityLogs);
        }
    }
}