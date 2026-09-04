using System;
using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public ProjectRole Role { get; set; } = ProjectRole.Member;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string Name { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; } = ProjectStatus.planning;
       
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    }
}