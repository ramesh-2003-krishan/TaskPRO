namespace TaskPRO.Application.features.Notifications.DTOs
{
    public class NotificationResponse
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}