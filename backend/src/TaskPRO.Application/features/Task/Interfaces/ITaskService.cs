using TaskPRO.Application.features.Task.DTOs;

namespace TaskPRO.Application.features.Task.Interfaces
{
    public interface ITaskService
    {
        Task<CreateTaskRequest> CreateTaskRequestAsync(CreateTaskRequest request, Guid projectId);
        Task<UpdateTaskRequest> UpdateTaskRequestAsync(UpdateTaskRequest request, Guid projectId);
        Task<AssignTaskRequest> AssignTaskRequestAsync(AssignTaskRequest request, Guid projectId, int taskItemId);
        Task<TaskResponse> TaskResponseAsync(int taskId, Guid projectId);
        Task<DeleteTaskRequest> DeleteTaskRequestAsync(DeleteTaskRequest request, Guid projectId, int taskItemId);
    }
}