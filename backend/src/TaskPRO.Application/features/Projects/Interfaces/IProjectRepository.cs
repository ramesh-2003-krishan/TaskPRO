using TaskPRO.Domain.entities;
using TaskPRO.Application.features.Projects.DTOs;

namespace TaskPRO.Application.features.Projects.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetProjectByIdAsync(Guid id);
        Task AddAsync(Project project);
        Task SaveChangesAsync();
        Task<Project?> GetMyProjectDetailByAsync(Guid projectId, Guid userId);
        Task<IEnumerable<Project>> SearchProjectsAsync(Guid CurrentUserId, string? searchTerm, int pageNumber, int pageSize);
        Task<IEnumerable<Project>> FilterProjectsAsync(Guid CurrentUserId, string? filterBy, int pageNumber, int pageSize);
        Task<IEnumerable<Project>> PaginateProjectsAsync(Guid CurrentUserId, int pageNumber, int pageSize);
        Task<ProjectMember?> GetProjectMemberByIdAsync(Guid projectId, Guid userId);
        Task AddProjectMemberAsync(ProjectMember projectMember);
        Task UpdateProjectMemberRoleAsync(ProjectMember projectMember, UpdateProjectRoleRequest request);
        Task RemoveProjectMemberAsync(ProjectMember projectMember);
    }
}