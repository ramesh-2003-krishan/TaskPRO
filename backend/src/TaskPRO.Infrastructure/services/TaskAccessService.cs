using Microsoft.EntityFrameworkCore;
using TaskPRO.Application.common.Exceptions;
using TaskPRO.Application.common.interfaces;
using TaskPRO.Domain.entities;
using TaskPRO.Infrastructure.data;
using TaskPRO.Infrastructure.Data;


namespace TaskPRO.Infrastructure.services
{
    public class TaskAccessService : ITaskAccessService
    {
        private readonly AppDBContext _db;

        public TaskAccessService(AppDBContext db)
        {
            _db = db;
        }

        public async Task<bool> HasAccessToTaskAsync(int taskId, Guid userId, CancellationToken ct = default)
        {
            var projectId = await _db.Tasks
                .AsNoTracking()
                .Where(t => t.Id == taskId)
                .Select(t => t.ProjectId)
                .FirstOrDefaultAsync(ct);

            if (projectId is null)
            {
                return false;
            }

            return await _db.ProjectMembers.AnyAsync(
                       pm => pm.ProjectId == projectId && pm.UserId == userId, ct)
                   || await _db.Projects.AnyAsync(
                       p => p.Id == projectId && p.UserId == userId, ct);
        }

        public async Task<TaskItem> GetTaskAsMemberAsync(int taskId, Guid userId, CancellationToken ct = default)
        {
            var task = await _db.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == taskId, ct);

            if (task == null)
            {
                throw new NotFoundException("Task not found");
            }

            var isMember = task.ProjectId is not null
                && (await _db.ProjectMembers.AnyAsync(
                        pm => pm.ProjectId == task.ProjectId && pm.UserId == userId, ct)
                    || await _db.Projects.AnyAsync(
                        p => p.Id == task.ProjectId && p.UserId == userId, ct));

            if (!isMember)
            {
                throw new ForbiddenException("You do not have access to this task");
            }

            return task;
        }
    }
}
