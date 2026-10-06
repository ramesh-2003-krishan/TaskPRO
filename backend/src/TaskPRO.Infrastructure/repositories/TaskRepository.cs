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
    }
}