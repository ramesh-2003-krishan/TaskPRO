using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using TaskPRO.Application.features.Projects.Interfaces;
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

        public  async Task<Project?> GetMyProjectDetailByAsync(Guid projectId, Guid userId)
        {
            return await _dbContext.Projects
                .Where(p => p.Id == projectId && p.ProjectMembers.Any(pm => pm.UserId == userId))
                .FirstOrDefaultAsync();
        }
    }
}