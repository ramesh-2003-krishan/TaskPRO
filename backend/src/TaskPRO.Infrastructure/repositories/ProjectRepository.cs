using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using TaskPRO.Application.features.Projects.Interfaces;
using TaskPRO.Application.features.Projects.DTOs;
using TaskPRO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskPRO.Infrastructure.repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDBContext _dbContext;

        public ProjectRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> GetProjectByIdAsync(Guid projectId)
        {
            return await _dbContext.Projects.FindAsync(projectId);
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(Project project)
        {
            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
        }

        public async Task AddProjectMemberAsync(ProjectMember projectMember)
        {
            await _dbContext.ProjectMembers.AddAsync(projectMember);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProjectMemberRoleAsync(
            ProjectMember projectMember,
            UpdateProjectRoleRequest request)
        {
            projectMember.Role = request.Role;
            _dbContext.ProjectMembers.Update(projectMember);
            await _dbContext.SaveChangesAsync();
        }

        public  async Task<Project?> GetMyProjectDetailByAsync(Guid projectId, Guid userId)
        {
            return await _dbContext.Projects
                .Where(p => p.Id == projectId && p.ProjectMembers.Any(pm => pm.UserId == userId))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Project>> SearchProjectsAsync(Guid CurrentUserId, string? searchTerm, int pageNumber, int pageSize)
        {
            var query = _dbContext.Projects
                .Where(p => p.ProjectMembers.Any(pm => pm.UserId == CurrentUserId));

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<Project>> FilterProjectsAsync(Guid CurrentUserId, string? filterBy, int pageNumber, int pageSize)
        {
            var query = _dbContext.Projects
                .Where(p => p.ProjectMembers.Any(pm => pm.UserId == CurrentUserId));

            if (!string.IsNullOrEmpty(filterBy))
            {
                if (Enum.TryParse<ProjectStatus>(filterBy, true, out var status))
                {
                    query = query.Where(p => p.Status == status);
                }
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<Project>> PaginateProjectsAsync(Guid CurrentUserId, int pageNumber, int pageSize)
        {
            return await _dbContext.Projects
                .Where(p => p.ProjectMembers.Any(pm => pm.UserId == CurrentUserId))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<ProjectMember?> GetProjectMemberByIdAsync(Guid projectId, Guid userId)
        {
            return await _dbContext.ProjectMembers
                .Where(pm => pm.ProjectId == projectId && pm.UserId == userId)
                .FirstOrDefaultAsync();
        }
        public async Task RemoveProjectMemberAsync(ProjectMember projectMember)
        {
            _dbContext.ProjectMembers.Remove(projectMember);
            await _dbContext.SaveChangesAsync();
        }
        public async Task ArchiveProjectAsync(Project project)
        {
            project.Status = ProjectStatus.Archived;
            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync();
        }
    }
}