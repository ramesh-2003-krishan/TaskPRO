using System;
using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class TaskComment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int TaskItemId { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
        public TaskItem? TaskItem { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}