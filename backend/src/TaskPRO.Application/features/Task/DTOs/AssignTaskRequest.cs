namespace TaskPRO.Application.features.Task.DTOs
{
    public class AssignTaskRequest
    {
        public Guid ProjectId { get; set;}
        public int TaskItemId { get; set; }
        public Guid UserId { get; set; }
    }
}