using Microsoft.AspNetCore.Mvc;
using TaskPRO.Application.common.Exceptions;
using TaskPRO.Application.features.Attachments.DTOs;
using TaskPRO.Application.features.Attachments.Interfaces;
using Microsoft.AspNetCore.Authorization;
using TaskPRO.Application.interfaces;

namespace TaskPRO.API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskAttachmentController : ControllerBase
    {
        private readonly ITaskAttachmentService _taskAttachmentService;
        private readonly ILogger<TaskAttachmentController> _logger;

        public TaskAttachmentController(ITaskAttachmentService taskAttachmentService, ILogger<TaskAttachmentController> logger)
        {
            _taskAttachmentService = taskAttachmentService;
            _logger = logger;
        }

        [HttpPost("{taskId}/upload")]
        [Authorize]
        public async Task<IActionResult> UploadAttachment(int taskId, IFormFile file, CancellationToken ct)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var stream = file.OpenReadStream();
            var result = await _taskAttachmentService.UploadAttachmentAsync(taskId, stream, file.FileName, ct);
            return Ok(result);
        }

        [HttpGet("{attachmentId}/download")]
        [Authorize]
        public async Task<IActionResult> DownloadAttachment(int attachmentId, CancellationToken ct)
        {
            try
            {
                var attachment = await _taskAttachmentService.DownloadAttachmentAsync(attachmentId, ct);
                return File(attachment.Content, attachment.FileName);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}