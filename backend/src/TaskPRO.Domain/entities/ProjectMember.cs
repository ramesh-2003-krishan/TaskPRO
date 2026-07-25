using System;
using TaskPRO.Domain.enums;

namespace TaskPRO.Domain.entities
{
    public class ProjectMember
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }

        public int ProjectId { get; set; }
        public ProjectprojectRole Role { get; set; }= ProjectprojectRole.Member;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User? User { get; set; }
        public Project? Project { get; set; }
    }
}