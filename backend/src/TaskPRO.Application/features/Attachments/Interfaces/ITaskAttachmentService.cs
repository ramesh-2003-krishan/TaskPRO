using TaskPRO.Application.features.Attachments.DTOs;
using TaskPRO.Domain.entities;

namespace TaskPRO.Application.features.Attachments.Interfaces
{
    public interface ITaskAttachmentService
    {
        Task<AttachmentResponse> UploadAttachmentAsync(int taskId, CancellationToken cancellationToken);
        Task<AttachmentDownload> DownloadAttachmentAsync(int attachmentId, CancellationToken cancellationToken);
        Task<bool> DeleteAttachmentAsync(int attachmentId, CancellationToken cancellationToken);
        Task<List<AttachmentResponse>> GetAttachmentsByTaskIdAsync(int taskId, CancellationToken cancellationToken);
    }
}