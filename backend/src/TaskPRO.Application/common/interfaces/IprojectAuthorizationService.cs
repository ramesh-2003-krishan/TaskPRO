using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.common.interfaces
{
    public interface IProjectAuthorizationService
    {
        Task<bool> CanViewProjectAsync(Guid projectId, Guid userId, bool isAdmin);
        Task<bool> CanEditProjectAsync(Guid projectId, Guid userId, bool isAdmin);
        Task<bool> CanDeleteProjectAsync(Guid projectId, Guid userId, bool isAdmin);
        Task<ProjectprojectRole?> GetUserRoleInProjectAsync(Guid projectId, Guid userId);
    }
}