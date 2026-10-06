using TaskPRO.Application.features.Task.DTOs;
using TaskPRO.Domain.entities;

namespace TaskPRO.Application.features.Task.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateTaskRequestAsync(TaskItem taskItem);
        Task<TaskItem> UpdateTaskRequestAsync(TaskItem taskItem);
        Task<TaskItem> AssignTaskRequestAsync(TaskItem taskItem);
        Task<TaskItem?> TaskResponseAsync(int taskId, Guid projectId);
        Task<TaskItem?> DeleteTaskRequestAsync(int taskId, Guid projectId);
    }
}