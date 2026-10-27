namespace TaskPRO.Application.features.ActivityLogs.DTOs
{
    public class ActivityLogResponse
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Action { get; set; }= null!;
        public string EntityName { get; set; }= null!;
        public string EntityId { get; set; }= null!;
        public DateTime Timestamp { get; set; }
    }
}