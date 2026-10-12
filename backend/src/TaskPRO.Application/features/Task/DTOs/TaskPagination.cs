using TaskPRO.Application.features.Task.DTOs;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.Task.DTOs
{
    public class TaskPagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<TaskResponse> TaskResponses { get; set; } = new();
    }
}