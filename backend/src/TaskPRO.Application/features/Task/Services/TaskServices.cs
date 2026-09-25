using TaskPRO.Application.features.Task.DTOs;
using TaskPRO.Application.features.Task.Interfaces;
using TaskPRO.Application.interfaces;
using TaskPRO.Domain.entities;


namespace TaskPRO.Application.features.Task.Services
{
    public class TaskServices : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICurrentUserService _currentUserService;

        public TaskServices(ITaskRepository taskRepository, ICurrentUserService currentUserService)
        {
            _taskRepository = taskRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CreateTaskRequest> CreateTaskRequestAsync(CreateTaskRequest request, Guid projectId)
        {
            var createdByUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("An authenticated user is required to create a task.");

            var taskItem = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                AssignedToUserId = request.AssignedToUserId,
                Priority = request.Priority,
                Status = request.TaskStatus,
                ProjectId = projectId,
                CreatedByUserId = createdByUserId
            };

            var createdTask = await _taskRepository.CreateTaskRequestAsync(taskItem);

            return new CreateTaskRequest
            {
                Title = createdTask.Title,
                Description = createdTask.Description,
                DueDate = createdTask.DueDate,
                AssignedToUserId = createdTask.AssignedToUserId,
                Priority = createdTask.Priority,
                TaskStatus = createdTask.Status
            };
        }
    }
}