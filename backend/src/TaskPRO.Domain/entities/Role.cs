using System;
using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public ProjectRole Name { get; set; }= ProjectRole.Member;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}