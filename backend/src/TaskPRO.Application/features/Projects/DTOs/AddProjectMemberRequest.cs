using System;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.Projects.DTOs
{
    public class AddProjectMemberRequest
    {
        public Guid UserId { get; set; }
        public ProjectRole Role { get; set; } = ProjectRole.Member;
    }
}