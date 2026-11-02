namespace TaskPRO.Application.features.ActivityLogs.DTOs
{
    public class ActivityLogResponse
    {
        public int Id { get; set; }
        public string ProjectId { get; set; } = null!;
        public string ProjectName { get; set; } = null!;
        public Guid UserId { get; set; } 
        public string UserName { get; set; } = null!;
        public string Action { get; set; }= null!;
        public string EntityName { get; set; }= null!;
        public Guid? EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<string> Details { get; set; } = new List<string>();
        
       
    }
}