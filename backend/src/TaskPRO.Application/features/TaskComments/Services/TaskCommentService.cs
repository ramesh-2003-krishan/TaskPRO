using TaskPRO.Application.features.TaskComments.Interfaces;
using TaskPRO.Application.features.TaskComments.DTOs;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.TaskComments.Services
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly ITaskCommentRepository _taskCommentRepository;
        private readonly ITaskCommentService _taskCommentService;

        public TaskCommentService(ITaskCommentRepository taskCommentRepository, ITaskCommentService taskCommentService)
        {
            _taskCommentRepository = taskCommentRepository;
            _taskCommentService = taskCommentService;
        }

        public async Task<TaskComment> CreateCommentAsync(CreateCommentRequest request, int taskId, Guid userId)
        {
            var comment = new TaskComment
            {
                Content = request.Content,
                TaskItemId = taskId,
            };

            return await _taskCommentRepository.CreateCommentAsync(comment);
        }

        public async Task<TaskComment> UpdateCommentAsync(UpdateCommentRequest request, Guid commentId, Guid userId)
        {
            var existingComment = await _taskCommentRepository.GetCommentByIdAsync(commentId);

            if (existingComment == null || existingComment.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this comment.");
            }

            existingComment.Content = request.Content;
           

            return await _taskCommentRepository.UpdateCommentAsync(existingComment);
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId, Guid userId)
        {
            var existingComment = await _taskCommentRepository.GetCommentByIdAsync(commentId);

            if (existingComment == null || existingComment.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this comment.");
            }

            return await _taskCommentRepository.DeleteCommentAsync(commentId);
        }

        public async Task<IEnumerable<TaskComment>> GetCommentsByTaskIdAsync(int taskId)
        {
            return await _taskCommentRepository.GetCommentsByTaskIdAsync(taskId);
        }
    }
}