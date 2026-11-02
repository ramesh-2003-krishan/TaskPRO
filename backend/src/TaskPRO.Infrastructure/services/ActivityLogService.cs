using TaskPRO.Application.features.ActivityLogs.DTOs;
using TaskPRO.Domain.entities;
using TaskPRO.Infrastructure.Data;
using TaskPRO.Application.common.interfaces;
using Microsoft.EntityFrameworkCore;


namespace TaskPRO.Infrastructure.services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly AppDBContext _dbContext;

        public ActivityLogService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LogActivityAsync(ActivityLog activityLogDTO)
        {
            var activityLog = new ActivityLog
            {
                UserId = activityLogDTO.UserId,
                Action = activityLogDTO.Action,
                EntityId = activityLogDTO.EntityId
            };

            _dbContext.ActivityLogs.Add(activityLog);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<ActivityLogResponse>> GetByProjectAsync(string entityName, string entityId, CancellationToken ct = default)
        {
            var activityLogs = await _dbContext.ActivityLogs
                .Where(a => a.Entity == entityName && a.EntityId.ToString() == entityId)
                .Select(a => new ActivityLogResponse
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    Action = a.Action,
                    EntityName = a.Entity,
                    EntityId = a.EntityId,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync(ct);

            return activityLogs;
        }
        public async Task<IEnumerable<ActivityLogResponse>> GetByTaskAsync(string userId, CancellationToken ct = default)
        {
            var activityLogs = await _dbContext.ActivityLogs
                .Where(a => a.UserId.ToString() == userId)
                .Select(a => new ActivityLogResponse
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    Action = a.Action,
                    EntityName = a.Entity,
                    EntityId = a.EntityId,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync(ct);

            return activityLogs;
        }
        public async Task LogActivityAsync(string userId, string action, string entityName, string entityId, CancellationToken ct = default)
        {
            var activityLog = new ActivityLog
            {
                UserId = Guid.Parse(userId),
                Action = action,
                Entity = entityName,
                EntityId = Guid.Parse(entityId),
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.ActivityLogs.Add(activityLog);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}