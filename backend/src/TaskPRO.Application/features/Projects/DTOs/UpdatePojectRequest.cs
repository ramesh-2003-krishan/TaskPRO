using System;
using System.ComponentModel.DataAnnotations;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.Projects.DTOs
{
    public class UpdateProjectRequest
    {
        [Required]
        public string ProjectName {get; set;} = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProjectStatus Status {get; set;}
        public DateTime? StartDate { get; set;}
        public DateTime? EndDate { get; set; }
    }
}