using TaskPRO.Application.features.TaskComments.DTOs;
using FluentValidation;
using TaskPRO.Application.features.TaskComments.Validators;
using TaskPRO.Application.features.TaskComments.Services;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using TaskPRO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.CodeAnalysis;
using TaskPRO.Application.features.TaskComments.Interfaces;

namespace TaskPRO.Infrastructure.repositories
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        private readonly AppDBContext _dbContext;

        public TaskCommentRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TaskComment> CreateCommentAsync(TaskComment comment)
        {
            _dbContext.TaskComments.Add(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<TaskComment> UpdateCommentAsync(TaskComment comment)
        {
            var existingComment = await _dbContext.TaskComments.FindAsync(comment.Id);
            if (existingComment is null)
            {
                throw new KeyNotFoundException($"Comment with ID {comment.Id} was not found.");
            }

            existingComment.Content = comment.Content;
            existingComment.UpdatedAt = DateTime.UtcNow;
            

            _dbContext.TaskComments.Update(existingComment);
            await _dbContext.SaveChangesAsync();
            return existingComment;
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId)
        {
            var existingComment = await _dbContext.TaskComments.FindAsync(commentId);
            if (existingComment is null)
            {
                throw new KeyNotFoundException($"Comment with ID {commentId} was not found.");
            }

            _dbContext.TaskComments.Remove(existingComment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<TaskComment?> GetCommentByIdAsync(Guid commentId)
        {
            var comment = await _dbContext.TaskComments.FindAsync(commentId);
            return comment;
        }

        public async Task<IEnumerable<TaskComment>> GetCommentsByTaskIdAsync(int taskId)
        {
            return await _dbContext.TaskComments
                .Where(comment => comment.TaskItemId == taskId)
                .ToListAsync();
        }
    }
}

