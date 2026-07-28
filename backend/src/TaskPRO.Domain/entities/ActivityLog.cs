using System;
using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public String Action { get; set; } = string.Empty;
        public String Entity { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}