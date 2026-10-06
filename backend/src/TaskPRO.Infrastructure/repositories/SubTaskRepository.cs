using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using TaskPRO.Application.features.SubTask.DTOs;
using TaskPRO.Application.features.SubTask.Interfaces;
using TaskPRO.Infrastructure.repositories;
using TaskPRO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;

namespace TaskPRO.Infrastructure.repositories
{
    public class SubTaskRepository : ISubTaskRepository
    {
        private readonly AppDBContext _dbContext;

        public SubTaskRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SubTask> CreateSubTaskAsync(SubTask subTask, Guid projectId, int taskItemId)
        {
            var existingTask = await _dbContext.Tasks.FindAsync(taskItemId);
            if (existingTask is null)
            {
                throw new KeyNotFoundException($"Task with ID {taskItemId} was not found.");
            }

            if (existingTask.ProjectId != projectId)
            {
                throw new KeyNotFoundException($"Task with ID {taskItemId} was not found in the specified project.");
            }

            subTask.TaskItemId = taskItemId;
            _dbContext.SubTasks.Add(subTask);
            await _dbContext.SaveChangesAsync();
            return subTask;
        }
        public async Task<SubTask> UpdateSubTaskAsync(SubTask subTask, Guid projectId, int taskItemId)
        {
            var existingTask = await _dbContext.Tasks.FindAsync(taskItemId);
            if (existingTask is null)
            {
                throw new KeyNotFoundException($"Task with ID {taskItemId} was not found.");
            }

            if (existingTask.ProjectId != projectId)
            {
                throw new KeyNotFoundException($"Task with ID {taskItemId} was not found in the specified project.");
            }

            var existingSubTask = await _dbContext.SubTasks.FindAsync(subTask.Id);
            if (existingSubTask is null)
            {
                throw new KeyNotFoundException($"SubTask with ID {subTask.Id} was not found.");
            }

            if (existingSubTask.TaskItemId != taskItemId)
            {
                throw new KeyNotFoundException($"SubTask with ID {subTask.Id} does not belong to the specified task.");
            }

            existingSubTask.Title = subTask.Title;
            existingSubTask.IsCompleted = subTask.IsCompleted;
            existingSubTask.UpdatedAt = DateTime.UtcNow;

            _dbContext.SubTasks.Update(existingSubTask);
            await _dbContext.SaveChangesAsync();
            return existingSubTask;
        }

        public async Task<SubTask?> SubTaskResponseAsync(int TaskId, Guid projectId)
        {
            var existingTask = await _dbContext.Tasks.FindAsync(TaskId);
            if (existingTask is null)
            {
                throw new KeyNotFoundException($"Task with ID {TaskId} was not found.");
            }

            if (existingTask.ProjectId != projectId)
            {
                throw new KeyNotFoundException($"Task with ID {TaskId} was not found in the specified project.");
            }
            
            return await _dbContext.SubTasks
                .FirstOrDefaultAsync(st => st.TaskItemId == TaskId);
        }
        public async Task<SubTask?> DeleteSubTaskAsync(SubTask subTask, Guid projectId, int taskItemId)
        {
            var existingTask = await _dbContext.Tasks.FindAsync(taskItemId);
            if (existingTask is null)
            {
                throw new KeyNotFoundException($"Task with ID {taskItemId} was not found.");
            }

            if (existingTask.ProjectId != projectId)
            {
                throw new KeyNotFoundException($"Task with ID {taskItemId} was not found in the specified project.");
            }

            var existingSubTask = await _dbContext.SubTasks.FindAsync(subTask.Id);
            if (existingSubTask is null)
            {
                throw new KeyNotFoundException($"SubTask with ID {subTask.Id} was not found.");
            }

            if (existingSubTask.TaskItemId != taskItemId)
            {
                throw new KeyNotFoundException($"SubTask with ID {subTask.Id} does not belong to the specified task.");
            }

            _dbContext.SubTasks.Remove(existingSubTask);
            await _dbContext.SaveChangesAsync();
            return existingSubTask;
        }
    }
}