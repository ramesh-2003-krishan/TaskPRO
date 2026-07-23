using System;
using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public ProjectprojectRole Role { get; set; } = ProjectprojectRole.Member;

        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public String Name { get; set; } = string.Empty;
        public String Status { get; set; } = string.Empty;
    }
}