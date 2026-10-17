using TaskPRO.Domain.entities;

namespace TaskPRO.Application.features.TaskComments.Interfaces
{
    public interface ITaskCommentRepository
    {
        Task<TaskComment> CreateCommentAsync(TaskComment comment);
        Task<TaskComment> UpdateCommentAsync(TaskComment comment);
        Task<bool> DeleteCommentAsync(Guid commentId);
        Task<TaskComment?> GetCommentByIdAsync(Guid commentId);
        Task<IEnumerable<TaskComment>> GetCommentsByTaskIdAsync(int taskId);
    }
}