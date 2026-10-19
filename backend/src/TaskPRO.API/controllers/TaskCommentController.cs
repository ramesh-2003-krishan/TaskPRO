using Microsoft.EntityFrameworkCore;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using TaskPRO.Infrastructure.Data;
using TaskPRO.Application.features.TaskComments.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using TaskPRO.Application.features.TaskComments.DTOs;
using TaskPRO.Application.features.TaskComments.Services;
using TaskPRO.Application.features.TaskComments.Validators;
using TaskPRO.Application.interfaces;
using Microsoft.AspNetCore.Authorization;


namespace TaskPRO.API.controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TaskCommentController : ControllerBase
    {
        private readonly ITaskCommentService _taskCommentService;
        private readonly ICurrentUserService _currentUserService;

        public TaskCommentController(ITaskCommentService taskCommentService, ICurrentUserService currentUserService)
        {
            _taskCommentService = taskCommentService;
            _currentUserService = currentUserService;
        }

        [HttpPost("{taskId:int}")]
        public async Task<ActionResult<TaskComment>> CreateComment([FromBody] CreateCommentRequest request, int taskId, [FromHeader] Guid userId)
        {
            var createdComment = await _taskCommentService.CreateCommentAsync(request, taskId, userId);
            return CreatedAtAction(nameof(CreateComment), new { id = createdComment.Id }, createdComment);
        }

        [HttpPut("{commentId:guid}")]
        public async Task<ActionResult<TaskComment>> UpdateComment([FromBody] UpdateCommentRequest request, Guid commentId)
        {
            var userId = _currentUserService.UserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var updatedComment = await _taskCommentService.UpdateCommentAsync(request, commentId, userId.Value);
            return Ok(updatedComment);
        }

        [HttpDelete("{commentId:guid}")]
        public async Task<ActionResult<bool>> DeleteComment(Guid commentId)
        {
            var userId = _currentUserService.UserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var result = await _taskCommentService.DeleteCommentAsync(commentId, userId.Value);
            return Ok(result);
        }

        [HttpGet("{commentId:guid}")]
        public async Task<ActionResult<TaskComment>> GetComment(Guid commentId)
        {
            var userId = _currentUserService.UserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var comment = await _taskCommentService.GetCommentByIdAsync(commentId, userId.Value);
            if (comment is null)
            {
                return NotFound();
            }

            return Ok(comment);
        }
    }
}