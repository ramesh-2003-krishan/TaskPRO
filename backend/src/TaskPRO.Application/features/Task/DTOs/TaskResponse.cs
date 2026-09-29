namespace TaskPRO.Application.features.Task.DTOs
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7));
        public Guid? AssignedToUserId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public TaskPRO.Domain.enums.Priority Priority { get; set; } = TaskPRO.Domain.enums.Priority.Low;
        public TaskPRO.Domain.enums.TaskStatus Status { get; set; } = TaskPRO.Domain.enums.TaskStatus.NotStarted;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
