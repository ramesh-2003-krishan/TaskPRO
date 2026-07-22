using System;
using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Revoked { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}