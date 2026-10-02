using TaskPRO.Application.features.SubTask.DTOs;

namespace TaskPRO.Application.features.SubTask.Interfaces
{
    public interface ISubTaskService
    {
        Task<CreateSubTaskRequest> CreateSubTaskAsync(CreateSubTaskRequest request, Guid projectId, int taskItemId);
    }
}