using TaskPRO.Application.common.Exceptions;
using TaskPRO.Application.features.ActivityLogs.DTOs;

namespace TaskPRO.Application.common.interfaces
{
    public interface IActivityLogService
    {
        Task LogActivityAsync(string userId, string action, string entityName, string entityId, CancellationToken ct = default);
        Task<IEnumerable<ActivityLogResponse>> GetByProjectAsync(string entityName, string entityId, CancellationToken ct = default);
        Task<IEnumerable<ActivityLogResponse>> GetByTaskAsync(string userId, CancellationToken ct = default);
    }
}