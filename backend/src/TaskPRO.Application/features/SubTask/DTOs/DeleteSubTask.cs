using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.SubTask.DTOs
{
    public class DeleteSubTaskRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}