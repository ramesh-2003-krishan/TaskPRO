using System.Collections.Generic;

namespace TaskPRO.Application.features.Projects.DTOs
{
    public class ProjectListResponse
    {
        public IEnumerable<ProjectResponse> Items { get; set; } = new List<ProjectResponse>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount/PageSize);
    }
}