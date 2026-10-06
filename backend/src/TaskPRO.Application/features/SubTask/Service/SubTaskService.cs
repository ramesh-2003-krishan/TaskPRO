using TaskPRO.Application.features.SubTask.DTOs;
using TaskPRO.Application.features.SubTask.Interfaces;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using SubTaskEntity = TaskPRO.Domain.entities.SubTask;

namespace TaskPRO.Application.features.SubTask.Service
{
    public class SubTaskService : ISubTaskService
    {
        private readonly ISubTaskRepository _subTaskRepository;

        public SubTaskService(ISubTaskRepository subTaskRepository)
        {
            _subTaskRepository = subTaskRepository;
        }

        public async Task<CreateSubTaskRequest> CreateSubTaskAsync(CreateSubTaskRequest request, Guid projectId, int taskItemId)
        {
            var subTaskEntity = new SubTaskEntity
            {
                Title = request.Title,
                IsCompleted = request.IsCompleted,
                TaskItemId = taskItemId,
                CreatedAt = DateTime.UtcNow
            };

            var createdSubTask = await _subTaskRepository.CreateSubTaskAsync(subTaskEntity, projectId, taskItemId);

            return new CreateSubTaskRequest
            {
                Id = createdSubTask.Id,
                Title = createdSubTask.Title,
                IsCompleted = createdSubTask.IsCompleted,
                TaskItemId = createdSubTask.TaskItemId,
                CreatedAt = createdSubTask.CreatedAt
            };
        }
        public async Task<UpdateSubTaskRequest> UpdateSubTaskAsync(UpdateSubTaskRequest request, Guid projectId, int taskItemId)
        {
            var subTaskEntity = new SubTaskEntity
            {
                Id = request.Id,
                Title = request.Title,
                IsCompleted = request.IsCompleted,
                TaskItemId = taskItemId,
                UpdatedAt = DateTime.UtcNow
            };

            var updatedSubTask = await _subTaskRepository.UpdateSubTaskAsync(subTaskEntity, projectId, taskItemId);

            return new UpdateSubTaskRequest
            {
                Id = updatedSubTask.Id,
                Title = updatedSubTask.Title,
                IsCompleted = updatedSubTask.IsCompleted,
                TaskItemId = updatedSubTask.TaskItemId,
                UpdatedAt = updatedSubTask.UpdatedAt
            };
        }

        public async Task<SubTaskResponse?> SubTaskResponseAsync(int taskId, Guid projectId)
        {
            var subTask = await _subTaskRepository.SubTaskResponseAsync(taskId, projectId);

            if (subTask is null)
            {
                return null;
            }

            return new SubTaskResponse
            {
                Id = subTask.Id,
                Title = subTask.Title,
                IsCompleted = subTask.IsCompleted,
                TaskItemId = subTask.TaskItemId,
                CreatedAt = subTask.CreatedAt,
                UpdatedAt = subTask.UpdatedAt
            };
        }

        public async Task<DeleteSubTaskRequest> DeleteSubTaskAsync(DeleteSubTaskRequest request,  int TaskItemId,Guid projectId)
        {
            var subTaskEntity = new SubTaskEntity
            {
                Id = request.Id,
                Title = request.Title,
                IsCompleted = request.IsCompleted,
                TaskItemId = TaskItemId,
                CreatedAt = request.CreatedAt
            };

            var deletedSubTask = await _subTaskRepository.DeleteSubTaskAsync(subTaskEntity, projectId, TaskItemId);

            if (deletedSubTask == null)
            {
                throw new KeyNotFoundException($"SubTask with ID {request.Id} was not found.");
            }

            return new DeleteSubTaskRequest
            {
                Id = deletedSubTask.Id,
                Title = deletedSubTask.Title,
                IsCompleted = deletedSubTask.IsCompleted,
                TaskItemId = deletedSubTask.TaskItemId,
                CreatedAt = deletedSubTask.CreatedAt
            };
        }
    }
}