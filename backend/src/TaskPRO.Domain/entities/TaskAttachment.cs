using System;
using TaskPRO.Domain.enums;
using TaskPRO.Domain.entities;

   public class TaskAttachment
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }