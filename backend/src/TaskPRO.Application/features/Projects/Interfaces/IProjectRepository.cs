using TaskPRO.Domain.entities;

namespace TaskPRO.Application.features.Projects.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetProjectByIdAsync(Guid id);
        Task AddAsync(Project project);
        Task SaveChangesAsync();
    }
}