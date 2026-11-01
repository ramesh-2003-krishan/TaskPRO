using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TaskPRO.Application.common.Exceptions;
using TaskPRO.Application.common.interfaces;
using TaskPRO.Application.common.Options;
using TaskPRO.Application.features.Attachments.DTOs;
using TaskPRO.Application.features.Attachments.Interfaces;
using TaskPRO.Domain.entities;
using TaskPRO.Infrastructure.Data;

namespace TaskPRO.Infrastructure.services
{
    public class TaskAttachmentService : ITaskAttachmentService
    {
        private readonly ILogger<TaskAttachmentService> _logger;
        private readonly IFileStorageService _fileStorageService;
        private readonly AppDBContext _dbContext;

        public TaskAttachmentService(
            ILogger<TaskAttachmentService> logger,
            IFileStorageService fileStorageService,
            AppDBContext dbContext)
        {
            _logger = logger;
            _fileStorageService = fileStorageService;
            _dbContext = dbContext;
        }

        public async Task<AttachmentResponse> UploadAttachmentAsync(int taskId, Stream content, string originalFileName, CancellationToken ct = default)
        {
            var task = await _dbContext.Tasks.FindAsync(new object[] { taskId }, ct);
            if (task == null)
                throw new NotFoundException($"Task with ID {taskId} not found.");

            var subFolder = $"tasks/{taskId}";
            var relativePath = await _fileStorageService.SaveAsync(content, originalFileName, subFolder, ct);

            var attachment = new TaskAttachment
            {
                Id = taskId,
                FileName = originalFileName,
                FilePath = relativePath,
                UploadedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _dbContext.TaskAttachments.Add(attachment);
            await _dbContext.SaveChangesAsync(ct);

            return new AttachmentResponse
            {
                Id = attachment.Id,
                FileName = attachment.FileName,
                FilePath = attachment.FilePath
            };
        }
        public async Task<AttachmentDownload> DownloadAttachmentAsync(int attachmentId, CancellationToken ct = default)
        {
            var attachment = await _dbContext.TaskAttachments.FindAsync(new object[] { attachmentId }, ct);
            if (attachment == null)
                throw new NotFoundException($"Attachment with ID {attachmentId} not found.");

            var stream = await _fileStorageService.OpenReadAsync(attachment.FilePath, ct);

            return new AttachmentDownload
            {
                FileName = attachment.FileName
            };
        }
        public async Task<bool> DeleteAttachmentAsync(int attachmentId, CancellationToken ct = default)
        {
            var attachment = await _dbContext.TaskAttachments.FindAsync(new object[] { attachmentId }, ct);
            if (attachment == null)
                throw new NotFoundException($"Attachment with ID {attachmentId} not found.");

            await _fileStorageService.DeleteAsync(attachment.FilePath, ct);

            _dbContext.TaskAttachments.Remove(attachment);
            await _dbContext.SaveChangesAsync(ct);

            return true;
        }
        public async Task<List<AttachmentResponse>> GetAttachmentsByTaskIdAsync(int taskId, CancellationToken ct = default)
        {
            var attachments = await _dbContext.TaskAttachments
                .Where(a => a.Id == taskId)
                .Select(a => new AttachmentResponse
                {
                    Id = a.Id,
                    FileName = a.FileName,
                    FilePath = a.FilePath
                })
                .ToListAsync(ct);

            return attachments;
        }
        public async Task<AttachmentResponse> UpdateAttachmentAsync(int attachmentId, string newFileName, Stream newContent, CancellationToken ct = default)
        {
            var attachment = await _dbContext.TaskAttachments.FindAsync(new object[] { attachmentId }, ct);
            if (attachment == null)
                throw new NotFoundException($"Attachment with ID {attachmentId} not found.");

           
            await _fileStorageService.DeleteAsync(attachment.FilePath, ct);

            
            var subFolder = $"tasks/{attachment.Id}";
            var relativePath = await _fileStorageService.SaveAsync(newContent, newFileName, subFolder, ct);

           
            attachment.FileName = newFileName;
            attachment.FilePath = relativePath;
            attachment.UpdatedAt = DateTime.UtcNow;

            _dbContext.TaskAttachments.Update(attachment);
            await _dbContext.SaveChangesAsync(ct);

            return new AttachmentResponse
            {
                Id = attachment.Id,
                FileName = attachment.FileName,
                FilePath = attachment.FilePath
            };
        }
    }
}