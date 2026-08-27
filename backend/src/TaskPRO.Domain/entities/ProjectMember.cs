using System;
using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class ProjectMember
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid ProjectId { get; set; }
        public ProjectRole Role { get; set; }= ProjectRole.Member;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User? User { get; set; }
        public Project? Project { get; set; }
    }
}