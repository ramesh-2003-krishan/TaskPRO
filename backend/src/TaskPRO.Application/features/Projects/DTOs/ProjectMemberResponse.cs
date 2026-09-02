using System.ComponentModel.DataAnnotations;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.Projects.DTOs
{
    public class ProjectMemberResponse
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ProjectRole Role { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}