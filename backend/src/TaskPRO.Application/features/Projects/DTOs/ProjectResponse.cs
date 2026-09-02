using System.ComponentModel.DataAnnotations;
using System.Data;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.Projects.DTOs
{
    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int MemberCount { get; set; }
        public List<ProjectMemberResponse> Members { get; set; } = new();
    }
}