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
    }
}