using System;
using System.Collections.Generic;
using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public String Title { get; set; } = string.Empty;
        public String Description { get; set; } = string.Empty;
        
        public TaskPRO.Domain.enums.TaskStatus Status { get; set; }= TaskPRO.Domain.enums.TaskStatus.NotStarted;
        public TaskPRO.Domain.enums.Priority Priority { get; set; }= TaskPRO.Domain.enums.Priority.Low;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7));
        public int? AssignedToUserId { get; set; }
        public int ProjectId { get; set; }
        public int CreatedByUserId { get; set; }

        public User? user { get; set; }
        public Project? project { get; set; }
        public User? createdByUser { get; set; }
        public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>(); 
        public ICollection<TaskAttachment> Attachments { get; set; } = new List<TaskAttachment>();
        public ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();

    }

    public class TaskComment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class TaskAttachment
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class SubTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

namespace TaskPRO.Domain.enums
{
    public class TaskPriority
    {
        public int Id {get; set;}
        public string Name {get; set;}= string.Empty;

        public TaskPRO.Domain.enums.Priority Priority {get; set;}= TaskPRO.Domain.enums.Priority.Low;
    }
}

