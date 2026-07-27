using System;
using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class Notification
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public String Title { get; set; } = string.Empty;
        public String Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; } = NotificationType.System;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}