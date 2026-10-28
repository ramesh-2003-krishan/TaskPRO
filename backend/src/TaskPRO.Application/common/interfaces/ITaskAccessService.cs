using TaskPRO.Domain.entities;

namespace TaskPRO.Application.common.interfaces
{
    public interface ITaskAccessService
    {
        Task<bool> HasAccessToTaskAsync(int taskId, Guid userId, CancellationToken ct = default);
        Task<TaskItem> GetTaskAsMemberAsync(int taskId, Guid userId, CancellationToken ct = default);
    }
}