using System;
using TaskPRO.Domain.enums;


namespace TaskPRO.Domain.entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public ProjectUserRole Name { get; set; } = ProjectUserRole.Role;

        public string UserEmail { get; set; } = string.Empty;

        public string PasswordHashedValue { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

        

        
    }
}