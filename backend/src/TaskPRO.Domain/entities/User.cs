using System;
using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class User
    {
        public Guid Id { get; set; }
        public ProjectUserRole Name { get; set; } = ProjectUserRole.Role;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}