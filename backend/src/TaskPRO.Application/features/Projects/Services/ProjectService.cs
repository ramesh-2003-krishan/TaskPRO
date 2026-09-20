using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskPRO.Application.features.Projects.DTOs;
using TaskPRO.Application.features.Projects.Interfaces;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using TaskPRO.Application.common.interfaces;


namespace TaskPRO.Application.features.Projects.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectAuthorizationService _projectAuthorizationService;
        public ProjectService(IProjectRepository projectRepository, IProjectAuthorizationService projectAuthorizationService)
        {
            _projectRepository = projectRepository;
            _projectAuthorizationService = projectAuthorizationService;
        }
        public async Task<ProjectResponse> CreateProjectAsync(Guid CurrentUserId, CreateProjectRequest request)
        {
            if(CurrentUserId == Guid.Empty)
            {
                throw new ArgumentException("Current user ID cannot be empty", nameof(CurrentUserId));
            }
            
            var newProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = request.ProjectName,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                Status = ProjectStatus.Active,
                UserId = CurrentUserId
            };

            var ownerMember = new ProjectMember
            {
                Id = Guid.NewGuid(),
                ProjectId = newProject.Id,
                UserId = CurrentUserId,
                Role = default(ProjectRole),
                CreatedAt = DateTime.UtcNow
            };

            
            newProject.ProjectMembers.Add(ownerMember);

            
            await _projectRepository.AddAsync(newProject);
            await _projectRepository.SaveChangesAsync();

            return new ProjectResponse
            {
                Id = newProject.Id,
                ProjectName = newProject.Name,
                Description = newProject.Description,
                Status = newProject.Status,
                OwnerId = newProject.UserId,
                CreatedAt = newProject.CreatedAt,
                Members = new List<ProjectMemberResponse>
                {
                    new ProjectMemberResponse
                    {
                        UserId = CurrentUserId,
                        Role = ownerMember.Role,
                        JoinedAt = ownerMember.CreatedAt
                    }
                }
            };
        }


        public async Task<ProjectDetailResponse> GetProjectByIdAsync(Guid projectId, Guid CurrentUserId, bool isAdmin)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);

            if (project == null)
            {
                throw new Exception("Project not found");
            }

            var CanAccess = isAdmin || await _projectAuthorizationService.GetProjectMemberAsync(projectId, CurrentUserId) != null;
            if (!CanAccess)
            {
                throw new Exception("You do not have access to this project");
            }

            return new ProjectDetailResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            };
        }

        public async Task<MyProjectDetailResponse> GetMyProjectDetailByAsync(Guid projectId, Guid userId)
        {
            var project = await _projectRepository.GetMyProjectDetailByAsync(projectId, userId);

            if (project == null)
            {
                throw new Exception("Project not found or you are not a member of this project");
            }

            return new MyProjectDetailResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            };
        }

        public async Task<IEnumerable<ProjectResponse>> SearchProjectsAsync(Guid CurrentUserId, string? searchTerm, int pageNumber, int pageSize)
        {
            var projects = await _projectRepository.SearchProjectsAsync(CurrentUserId, searchTerm, pageNumber, pageSize);

            return projects.Select(project => new ProjectResponse
            {
                Id = project.Id,
                ProjectName = project.Name,
                Description = project.Description,
                Status = project.Status,
                OwnerId = project.UserId,
                CreatedAt = project.CreatedAt,
                Members = project.ProjectMembers.Select(pm => new ProjectMemberResponse
                {
                    UserId = pm.UserId,
                    Role = pm.Role,
                    JoinedAt = pm.CreatedAt
                }).ToList()
            });
        }
        public async Task<IEnumerable<ProjectResponse>> FilterProjectsAsync(Guid CurrentUserId, string? filterBy, int pageNumber, int pageSize)
        {
            var projects = await _projectRepository.FilterProjectsAsync(CurrentUserId, filterBy, pageNumber, pageSize);

            return projects.Select(project => new ProjectResponse
            {
                Id = project.Id,
                ProjectName = project.Name,
                Description = project.Description,
                Status = project.Status,
                OwnerId = project.UserId,
                CreatedAt = project.CreatedAt,
                Members = project.ProjectMembers.Select(pm => new ProjectMemberResponse
                {
                    UserId = pm.UserId,
                    Role = pm.Role,
                    JoinedAt = pm.CreatedAt
                }).ToList()
            });
        }
        public async Task<IEnumerable<ProjectResponse>> PaginateProjectsAsync(Guid CurrentUserId, int pageNumber, int pageSize)
        {
            var projects = await _projectRepository.PaginateProjectsAsync(CurrentUserId, pageNumber, pageSize);

            return projects.Select(project => new ProjectResponse
            {
                Id = project.Id,
                ProjectName = project.Name,
                Description = project.Description,
                Status = project.Status,
                OwnerId = project.UserId,
                CreatedAt = project.CreatedAt,
                Members = project.ProjectMembers.Select(pm => new ProjectMemberResponse
                {
                    UserId = pm.UserId,
                    Role = pm.Role,
                    JoinedAt = pm.CreatedAt
                }).ToList()
            });
        }
        public async Task<ProjectMemberResponse?> GetProjectMemberByIdAsync(Guid projectId, Guid userId)
        {
            var projectMember = await _projectRepository.GetProjectMemberByIdAsync(projectId, userId);

            if (projectMember == null)
            {
                return null;
            }

            return new ProjectMemberResponse
            {
                UserId = projectMember.UserId,
                Role = projectMember.Role,
                JoinedAt = projectMember.CreatedAt
            };
        }
        public async Task<ProjectResponse> UpdateProjectAsync(Guid CurrentUserId, Guid projectId, UpdateProjectRequest request)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);

            if (project == null)
            {
                throw new Exception("Project not found");
            }

            
            var isMember = project.ProjectMembers.Any(pm => pm.UserId == CurrentUserId);
            if (!isMember)
            {
                throw new Exception("You are not a member of this project");
            }

            var isValidRole = await _projectAuthorizationService.GetUserRoleInProjectAsync(projectId, CurrentUserId);
            if(isValidRole != ProjectprojectRole.Owner && isValidRole != ProjectprojectRole.Manager)
            {
                throw new Exception("You do not have permission to update this project");
            }

            
            project.Name = request.ProjectName;
            project.Description = request.Description;
            project.Status = request.Status;
            project.UpdatedAt = DateTime.UtcNow;

            await _projectRepository.SaveChangesAsync();

            return new ProjectResponse
            {
                Id = project.Id,
                ProjectName = project.Name,
                Description = project.Description,
                Status = project.Status,
                OwnerId = project.UserId,
                CreatedAt = project.CreatedAt,
                Members = project.ProjectMembers.Select(pm => new ProjectMemberResponse
                {
                    UserId = pm.UserId,
                    Role = pm.Role,
                    JoinedAt = pm.CreatedAt
                }).ToList()
            };
        }
        public async Task<ProjectMemberResponse> AddProjectMemberAsync(Guid projectId, Guid userId, string role)
        {
            var projectMemberExists = await _projectRepository.GetProjectMemberByIdAsync(projectId, userId);
            if(projectMemberExists != null)
            {
                throw new Exception("User is already a member of this project");
            }
            
            var isValidRole = await _projectAuthorizationService.GetUserRoleInProjectAsync(projectId, userId);
            if(isValidRole != ProjectprojectRole.Owner && isValidRole != ProjectprojectRole.Manager)
            {
                throw new Exception("You do not have permission to update this project");
            }

            var projectMember = new ProjectMember
            {
                Id = Guid.NewGuid(),
                ProjectId = projectId,
                UserId = userId,
                Role = Enum.TryParse<ProjectRole>(role, true, out var parsedRole) ? parsedRole : throw new ArgumentException("Invalid role", nameof(role)),
                CreatedAt = DateTime.UtcNow
            };      
            if (projectMember == null)
            {
                throw new ArgumentNullException(nameof(projectMember));
            }
            return new ProjectMemberResponse
            {
                UserId = projectMember.UserId,
                Role = projectMember.Role,
                JoinedAt = projectMember.CreatedAt
            };
            
        }
        public async Task<ProjectMemberResponse> UpdateProjectMemberRoleAsync(Guid projectId, Guid userId, UpdateProjectRoleRequest request)
        {
            var projectMember = await _projectRepository.GetProjectMemberByIdAsync(projectId, userId);

            if (projectMember == null)
            {
                throw new Exception("Project member not found");
            }

            var isValidRole = await _projectAuthorizationService.GetUserRoleInProjectAsync(projectId, userId);
            if(isValidRole != ProjectprojectRole.Owner && isValidRole != ProjectprojectRole.Manager)
            {
                throw new Exception("You do not have permission to update this project member's role");
            }

            await _projectRepository.UpdateProjectMemberRoleAsync(projectMember, request);

            return new ProjectMemberResponse
            {
                UserId = projectMember.UserId,
                Role = projectMember.Role,
                JoinedAt = projectMember.CreatedAt
            };
        }
        public async Task<ProjectResponse> ArchiveProjectAsync(Guid CurrentUserId, Guid projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);

            if (project == null)
            {
                throw new Exception("Project not found");
            }

            
            var isMember = project.ProjectMembers.Any(pm => pm.UserId == CurrentUserId);
            if (!isMember)
            {
                throw new Exception("You are not a member of this project");
            }

            
            project.Status = ProjectStatus.Archived;
            project.UpdatedAt = DateTime.UtcNow;

            await _projectRepository.ArchiveProjectAsync(project);
            await _projectRepository.SaveChangesAsync();

            return new ProjectResponse
            {
                Id = project.Id,
                ProjectName = project.Name,
                Description = project.Description,
                Status = project.Status,
                OwnerId = project.UserId,
                CreatedAt = project.CreatedAt,
                Members = project.ProjectMembers.Select(pm => new ProjectMemberResponse
                {
                    UserId = pm.UserId,
                    Role = pm.Role,
                    JoinedAt = pm.CreatedAt
                }).ToList()
            };
        }

        public async Task<ProjectMemberResponse> RemoveProjectMemberAsync(Guid projectId, Guid userId)
        {
            var projectMember = await _projectRepository.GetProjectMemberByIdAsync(projectId, userId);

            if (projectMember == null)
            {
                throw new Exception("Project member not found");
            }

            var isValidRole = await _projectAuthorizationService.GetUserRoleInProjectAsync(projectId, userId);
            if(isValidRole != ProjectprojectRole.Owner && isValidRole != ProjectprojectRole.Manager)
            {
                throw new Exception("You do not have permission to remove this project member");
            }

            await _projectRepository.RemoveProjectMemberAsync(projectMember);

            return new ProjectMemberResponse
            {
                UserId = projectMember.UserId,
                Role = projectMember.Role,
                JoinedAt = projectMember.CreatedAt
            };
        }
        public async Task<ProjectResponse> DeleteProjectAsync(Guid CurrentUserId, Guid projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);

            if (project == null)
            {
                throw new Exception("Project not found");
            }

            
            var isMember = project.ProjectMembers.Any(pm => pm.UserId == CurrentUserId);
            if (!isMember)
            {
                throw new Exception("You are not a member of this project");
            }

            
            await _projectRepository.DeleteProjectAsync(project);
            await _projectRepository.SaveChangesAsync();

            return new ProjectResponse
            {
                Id = project.Id,
                ProjectName = project.Name,
                Description = project.Description,
                Status = project.Status,
                OwnerId = project.UserId,
                CreatedAt = project.CreatedAt,
                Members = project.ProjectMembers.Select(pm => new ProjectMemberResponse
                {
                    UserId = pm.UserId,
                    Role = pm.Role,
                    JoinedAt = pm.CreatedAt
                }).ToList()
            };
        }
       
    }
}