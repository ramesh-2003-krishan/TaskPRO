using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using TaskPRO.Application.features.Task.DTOs;
using TaskPRO.Application.features.Task.Interfaces;
using TaskPRO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
    }
}