
using TaskPRO.Domain.enums;
using TaskPRO.Domain.entities;


namespace TaskPRO.Application.common.interfaces
{
    public interface IProjectAuthorizationService
    {
        Task<bool> CanViewProjectAsync(Guid projectId, Guid userId, bool isAdmin);
        Task<bool> CanEditProjectAsync(Guid projectId, Guid userId, bool isAdmin);
        Task<bool> CanDeleteProjectAsync(Guid projectId, Guid userId, bool isAdmin);
        Task<ProjectRole?> GetUserRoleInProjectAsync(Guid projectId, Guid userId);
        Task<ProjectMember?> GetProjectMemberAsync(Guid projectId, Guid userId);
        
    }
}