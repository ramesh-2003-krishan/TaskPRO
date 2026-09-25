using TaskPRO.Application.features.Task.DTOs;

namespace TaskPRO.Application.features.Task.Interfaces
{
    public interface ITaskService
    {
        Task<CreateTaskRequest> CreateTaskRequestAsync(CreateTaskRequest request, Guid projectId);

    }
}