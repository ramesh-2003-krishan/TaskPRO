using TaskPRO.Application.features.Task.DTOs;
using TaskPRO.Domain.entities;

namespace TaskPRO.Application.features.Task.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateTaskRequestAsync(TaskItem taskItem);
    }
}