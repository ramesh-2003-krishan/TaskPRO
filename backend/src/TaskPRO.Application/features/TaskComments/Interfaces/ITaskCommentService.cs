using TaskPRO.Application.features.TaskComments.DTOs;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.TaskComments.Interfaces
{
    public interface ITaskCommentService
    {
        Task<TaskComment> CreateCommentAsync(CreateCommentRequest request, int taskId, Guid userId);
        Task<TaskComment> UpdateCommentAsync(UpdateCommentRequest request, Guid commentId, Guid userId);
        Task<bool> DeleteCommentAsync(Guid commentId, Guid userId);
        Task<TaskComment?> GetCommentByIdAsync(Guid commentId, Guid userId);
        Task<IEnumerable<TaskComment>> GetCommentsByTaskIdAsync(int taskId);
    }
}