using TaskPRO.Domain.entities;

namespace TaskPRO.Application.common.interfaces
{
    public interface ITaskAccessService
    {
        Task<bool> HasAccessToTaskAsync(Guid taskId, Guid userId);
        Task<TaskItem> GetTaskAsMemberAsync(Guid taskId, Guid userId, CancellationToken ct = default); 
    }
}