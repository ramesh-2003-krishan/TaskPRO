using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using TaskPRO.Application.features.Task.DTOs;
using TaskPRO.Application.features.Task.Interfaces;
using TaskPRO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;

namespace TaskPRO.Infrastructure.repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDBContext _dbContext;

        public TaskRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TaskItem> CreateTaskRequestAsync(TaskItem taskItem)
        {
            _dbContext.Tasks.Add(taskItem);
            await _dbContext.SaveChangesAsync();
            return taskItem;
        }
        public async Task<TaskItem> UpdateTaskRequestAsync(TaskItem taskItem)
        {
            _dbContext.Tasks.Update(taskItem);
            await _dbContext.SaveChangesAsync();
            return taskItem;
        }
        public async Task<TaskItem> AssignTaskRequestAsync(TaskItem taskItem)
        {
            var existingTask = await _dbContext.Tasks.FindAsync(taskItem.Id);
            if (existingTask is null)
            {
                throw new KeyNotFoundException($"Task with ID {taskItem.Id} was not found.");
            }

            if (existingTask.ProjectId != taskItem.ProjectId)
            {
                throw new KeyNotFoundException($"Task with ID {taskItem.Id} was not found in the specified project.");
            }

            existingTask.AssignedToUserId = taskItem.AssignedToUserId;
            existingTask.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return existingTask;
        }
        public async Task<TaskItem?> TaskResponseAsync(int taskId, Guid projectId)
        {
            return await _dbContext.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId && t.ProjectId == projectId);
        }
        public async Task<TaskItem?> DeleteTaskRequestAsync(int taskId, Guid projectId)
        {
            var taskItem = await _dbContext.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId && t.ProjectId == projectId);

            if (taskItem is null)
            {
                return null;
            }

            _dbContext.Tasks.Remove(taskItem);
            await _dbContext.SaveChangesAsync();
            return taskItem;
        }
        public async Task<IEnumerable<TaskItem>> SortTaskRequestAsync(SortTaskRequest request, Guid projectId)
        {
            var tasksQuery = _dbContext.Tasks.AsQueryable();

            
            tasksQuery = tasksQuery.Where(t => t.ProjectId == projectId);

            
            tasksQuery = request.SortBy switch
            {
                "Title" => request.SortOrder == global::TaskPRO.Application.features.Task.DTOs.SortOrder.Asc ? tasksQuery.OrderBy(t => t.Title) : tasksQuery.OrderByDescending(t => t.Title),
                "DueDate" => request.SortOrder == global::TaskPRO.Application.features.Task.DTOs.SortOrder.Asc ? tasksQuery.OrderBy(t => t.DueDate) : tasksQuery.OrderByDescending(t => t.DueDate),
                "Priority" => request.SortOrder == global::TaskPRO.Application.features.Task.DTOs.SortOrder.Asc ? tasksQuery.OrderBy(t => t.Priority) : tasksQuery.OrderByDescending(t => t.Priority),
                "Status" => request.SortOrder == global::TaskPRO.Application.features.Task.DTOs.SortOrder.Asc ? tasksQuery.OrderBy(t => t.Status) : tasksQuery.OrderByDescending(t => t.Status),
                _ => throw new ArgumentException($"Invalid SortBy value: {request.SortBy}")
            };

            return await tasksQuery.ToListAsync();
        }
        public async Task<TaskItem?> UnAssignTaskRequestAsync(int taskId, Guid projectId)
        {
            var existingTask = await _dbContext.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId && t.ProjectId == projectId);

            if (existingTask is null)
            {
                return null;
            }

            existingTask.AssignedToUserId = null;
            existingTask.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return existingTask;
        }
    }
}