using TaskPRO.Domain.entities;
using TaskPRO.Application.features.Projects.DTOs;

namespace TaskPRO.Application.features.Projects.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetProjectByIdAsync(Guid id);
        System.Threading.Tasks.Task AddAsync(Project project);
        System.Threading.Tasks.Task SaveChangesAsync();
        Task<Project?> GetMyProjectDetailByAsync(Guid projectId, Guid userId);
        Task<IEnumerable<Project>> SearchProjectsAsync(Guid CurrentUserId, string? searchTerm, int pageNumber, int pageSize);
        Task<IEnumerable<Project>> FilterProjectsAsync(Guid CurrentUserId, string? filterBy, int pageNumber, int pageSize);
        Task<IEnumerable<Project>> PaginateProjectsAsync(Guid CurrentUserId, int pageNumber, int pageSize);
        Task<ProjectMember?> GetProjectMemberByIdAsync(Guid projectId, Guid userId);
        System.Threading.Tasks.Task AddProjectMemberAsync(ProjectMember projectMember);
        System.Threading.Tasks.Task UpdateProjectMemberRoleAsync(ProjectMember projectMember, UpdateProjectRoleRequest request);
        System.Threading.Tasks.Task RemoveProjectMemberAsync(ProjectMember projectMember);
        System.Threading.Tasks.Task ArchiveProjectAsync(Project project);
        System.Threading.Tasks.Task DeleteProjectAsync(Project project);
    }
}