using System;
using System.Threading.Tasks;
using TaskPRO.Application.features.Projects.DTOs;

namespace TaskPRO.Application.features.Projects.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectResponse> CreateProjectAsync (Guid CurrentUserId,CreateProjectRequest request);
        Task<ProjectDetailResponse> GetProjectByIdAsync(Guid projectId);
        Task<MyProjectDetailResponse> GetMyProjectDetailByAsync(Guid projectId, Guid userId);
        Task<IEnumerable<ProjectResponse>> SearchProjectsAsync(Guid CurrentUserId, string? searchTerm, int pageNumber, int pageSize);
        Task<IEnumerable<ProjectResponse>> FilterProjectsAsync(Guid CurrentUserId, string? filterBy, int pageNumber, int pageSize);
        Task<IEnumerable<ProjectResponse>> PaginateProjectsAsync(Guid CurrentUserId, int pageNumber, int pageSize);
        Task<ProjectMemberResponse?> GetProjectMemberByIdAsync(Guid projectId, Guid userId);
        Task<ProjectResponse> UpdateProjectAsync(Guid CurrentUserId, Guid projectId, UpdateProjectRequest request);
        Task<ProjectMemberResponse> AddProjectMemberAsync(Guid projectId, Guid userId, string role);
    }
}