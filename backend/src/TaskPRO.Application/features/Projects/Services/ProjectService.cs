using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskPRO.Application.features.Projects.DTOs;
using TaskPRO.Application.features.Projects.Interfaces;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.Projects.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<ProjectResponse> CreateProjectAsync(Guid CurrentUserId, CreateProjectRequest request)
        {
            
            var newProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = request.ProjectName,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                Status = ProjectStatus.Active
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


        public async Task<ProjectDetailResponse> GetProjectByIdAsync(Guid projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);

            if (project == null)
            {
                throw new Exception("Project not found");
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
    }
}