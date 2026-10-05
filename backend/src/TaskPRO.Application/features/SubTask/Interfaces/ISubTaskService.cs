using TaskPRO.Application.features.SubTask.DTOs;

namespace TaskPRO.Application.features.SubTask.Interfaces
{
    public interface ISubTaskService
    {
        Task<CreateSubTaskRequest> CreateSubTaskAsync(CreateSubTaskRequest request, Guid projectId, int taskItemId);
        Task<UpdateSubTaskRequest> UpdateSubTaskAsync(UpdateSubTaskRequest request, Guid projectId, int taskItemId);
        Task<SubTaskResponse> SubTaskResponseAsync (int TaskId, Guid projectId);
    }
}