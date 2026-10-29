using TaskPRO.Domain.entities;

namespace TaskPRO.Application.features.Attachments.DTOs
{
    public class AttachmentResponse
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public long FileSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public int TaskId { get; set; }

    }
    public class AttachmentDownload
    {
        public Stream Content { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public long FileSize { get; set; }
    }
}