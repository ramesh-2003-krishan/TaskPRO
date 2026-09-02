using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.Projects.DTOs
{
    public class ProjectListRequest
    {
        public string? SearchTerm { get; set; }
        public ProjectStatus Status { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}