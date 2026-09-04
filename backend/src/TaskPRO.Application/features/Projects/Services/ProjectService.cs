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
    }
}