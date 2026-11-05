using TaskPRO.Application.features.Task.DTOs;
using TaskPRO.Application.features.Task.Interfaces;
using TaskPRO.Application.interfaces;
using TaskPRO.Domain.entities;
using TaskPRO.Application.common.interfaces;


namespace TaskPRO.Application.features.Task.Services
{
    public class TaskServices : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IProjectAuthorizationService _projectAuthorizationService;
        private readonly IActivityLogService _activityLogService;
        private readonly INotificationService _notificationService;

        public TaskServices(ITaskRepository taskRepository, 
                            ICurrentUserService currentUserService, 
                            IProjectAuthorizationService projectAuthorizationService,
                            IActivityLogService activityLogService,
                            INotificationService notificationService)
        {
            _taskRepository = taskRepository;
            _currentUserService = currentUserService;
            _projectAuthorizationService = projectAuthorizationService;
            _activityLogService = activityLogService;
            _notificationService = notificationService;
        }

        private async Task<Guid> RequireMembershipAsync(Guid projectId)
        {
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("An authenticated user is required.");

            if (!await _projectAuthorizationService.CanViewProjectAsync(projectId, currentUserId, isAdmin: false))
            {
                throw new UnauthorizedAccessException("You do not have permission to view this project.");
            }
           
            return currentUserId;
        }
        private async Task<Guid> RequireEditPermissionAsync(Guid projectId)
        {
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("An authenticated user is required.");

            if (!await _projectAuthorizationService.CanEditProjectAsync(projectId, currentUserId, isAdmin: false))
            {
                throw new UnauthorizedAccessException("You do not have permission to edit this project.");
            }

            return currentUserId;
        }
        private async Task<bool> EnsureAssigneeIsMemberOfProjectAsync(Guid projectId, Guid assigneeUserId)
        {
            var assigneeMember = await _projectAuthorizationService.GetProjectMemberAsync(projectId, assigneeUserId);
            if (assigneeMember == null)
            {
                throw new InvalidOperationException("The user being assigned to the task is not a member of the project.");
            }
            return true;
        }
        

        public async Task<CreateTaskRequest> CreateTaskRequestAsync(CreateTaskRequest request, Guid projectId)
        {
            var createdByUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("An authenticated user is required to create a task.");
            await RequireEditPermissionAsync(projectId);
            
            if (request.AssignedToUserId.HasValue)
            {
                await EnsureAssigneeIsMemberOfProjectAsync(projectId, request.AssignedToUserId.Value);
            }
            

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
            await RequireEditPermissionAsync(projectId);
            
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
            await RequireMembershipAsync(projectId);
            await EnsureAssigneeIsMemberOfProjectAsync(projectId, request.UserId);
            await RequireEditPermissionAsync(projectId);

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
            await RequireEditPermissionAsync(projectId);
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
        public async Task<SortTaskRequest> SortTaskRequestAsync(SortTaskRequest request, Guid projectId)
        {
            await RequireEditPermissionAsync(projectId);
            var sortedTasks = await _taskRepository.SortTaskRequestAsync(request, projectId);

            return new SortTaskRequest
            {
                SortBy = request.SortBy,
                SortOrder = request.SortOrder
            };
        }
        public async Task<UnAssignTaskRequest> UnAssignTaskRequestAsync(UnAssignTaskRequest request, Guid projectId, int taskId)
        {
            var unassignedTask = await _taskRepository.UnAssignTaskRequestAsync(taskId, projectId);

            if (unassignedTask == null)
            {
                throw new KeyNotFoundException($"Task with ID {taskId} was not found in project {projectId}.");
            }

            return new UnAssignTaskRequest
            {
                TaskId = unassignedTask.Id
            };
        }
        public async Task<TaskPagination> GetTasksByProjectIdAsync(Guid projectId, int pageNumber, int pageSize)
        {
            var tasks = await _taskRepository.TaskPaginationAsync(projectId, pageNumber, pageSize);

            return new TaskPagination
            {
            PageNumber = pageNumber,
            PageSize = pageSize,
                TaskResponses = tasks.Select(task => new TaskResponse
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
                }).ToList()
            };
        }
        public async Task<int> GetTotalTaskCountAsync(Guid projectId)
        {
            return await _taskRepository.GetTotalTaskCountAsync(projectId);
        }
    }
}