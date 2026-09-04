using System;
using System.Threading.Tasks;
using TaskPRO.Application.features.Projects.DTOs;

namespace TaskPRO.Application.features.Projects.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectResponse> CreateProjectAsync (Guid CurrentUserId,CreateProjectRequest request);
    }
}