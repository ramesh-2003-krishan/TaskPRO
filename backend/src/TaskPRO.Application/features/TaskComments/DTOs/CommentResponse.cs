namespace TaskPRO.Application.features.TaskComments.DTOs
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
    }
}