using System.ComponentModel.DataAnnotations;

namespace TaskPRO.Application.features.Projects.DTOs
{
    public class CreateProjectRequest
    {
        [Required]
        public string ProjectName {get; set;} = string.Empty;

        [Required]
        public string Description { get; set;} = string.Empty;

        [Required]
        public DateTime? StartDate {get; set;}

        [Required]
        public DateTime? EndDate {get; set;}
    }
}