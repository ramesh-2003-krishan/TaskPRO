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
        public async Task<UpdateTaskRequest> UpdateTaskRequestAsync(UpdateTaskRequest request, Guid projectId)
        {
            var taskItem = new TaskItem
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                AssignedToUserId = request.AssignedToUserId,
                Priority = request.Priority,
                Status = request.TaskStatus,
                ProjectId = projectId 
            };

            var updatedTask = await _taskRepository.UpdateTaskRequestAsync(taskItem);

            return new UpdateTaskRequest
            {
               Id = updatedTask.Id,
               Title = updatedTask.Title,
               Description = updatedTask.Description,
               DueDate = updatedTask.DueDate,
               AssignedToUserId = updatedTask.AssignedToUserId,
               Priority = updatedTask.Priority,
               TaskStatus = updatedTask.Status
            };
        }

        public async Task<AssignTaskRequest> AssignTaskRequestAsync(AssignTaskRequest request, Guid projectId, int taskId)
        {
            var taskItem = new TaskItem
            {
                Id = taskId,
                ProjectId = projectId,
                AssignedToUserId = request.UserId
            };

            var assignedTask = await _taskRepository.AssignTaskRequestAsync(taskItem);

            return new AssignTaskRequest
            {
                ProjectId = projectId,
                TaskItemId = assignedTask.Id,
                UserId = assignedTask.AssignedToUserId
                    ?? throw new InvalidOperationException("The task assignment was not saved.")
            };
        }
        public async Task<TaskResponse> TaskResponseAsync(int taskId, Guid projectId)
        {
            var task = await _taskRepository.TaskResponseAsync(taskId, projectId);

            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {taskId} was not found in project {projectId}.");
            }

            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                AssignedToUserId = task.AssignedToUserId,
                ProjectId = task.ProjectId,
                CreatedByUserId = task.CreatedByUserId,
                Priority = task.Priority,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }
        public async Task<DeleteTaskRequest> DeleteTaskRequestAsync(DeleteTaskRequest request, Guid projectId, int taskId)
        {
            var deletedTask = await _taskRepository.DeleteTaskRequestAsync(taskId, projectId);

            if (deletedTask == null)
            {
                throw new KeyNotFoundException($"Task with ID {taskId} was not found in project {projectId}.");
            }

            return new DeleteTaskRequest
            {
                Id = deletedTask.Id
            };
        }
    }
}