using System;
using System.Threading.Tasks;
using TaskPRO.Application.features.Projects.DTOs;
using TaskPRO.Application.features.Projects.Interfaces;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.Projects.DTOs
{
    public class ProjectDetailResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; }= ProjectStatus.Active;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
       
    }

    public class ProjectOwnerDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}