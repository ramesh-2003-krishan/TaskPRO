using TaskPRO.Application.common.interfaces;
using TaskPRO.Domain.enums;
using TaskPRO.Infrastructure.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskPRO.Domain.entities;


namespace TaskPRO.Infrastructure.services
{
    public class ProjectAuthorizationService : IProjectAuthorizationService
    {
        private readonly AppDBContext _dbContext;
        public ProjectAuthorizationService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
         public async Task<ProjectRole?> GetUserRoleInProjectAsync(Guid projectId, Guid userId)
        {
            var projectMember = await _dbContext.ProjectMembers
                .AsNoTracking()
                .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);

            return ProjectRole.Member; 
        }
        public async Task<bool> CanViewProjectAsync(Guid projectId, Guid userId, bool isAdmin)
        {
            if (isAdmin) return true;

            var role = await GetUserRoleInProjectAsync(projectId, userId);
            return role != null;
        }

        public async Task<bool> CanEditProjectAsync(Guid projectId, Guid userId, bool isAdmin)
        {
            if (isAdmin) return true;

            var role = await GetUserRoleInProjectAsync(projectId, userId);
            return role == ProjectRole.Admin || role == ProjectRole.ProjectManager;
        }

        public async Task<bool> CanDeleteProjectAsync(Guid projectId, Guid userId, bool isAdmin)
        {
            if (isAdmin) return true;

            var role = await GetUserRoleInProjectAsync(projectId, userId);
            return role == ProjectRole.Admin;
        }
        public async Task<ProjectMember?> GetProjectMemberAsync(Guid projectId, Guid userId)
        {
         return await _dbContext.ProjectMembers
                .AsNoTracking()
                .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);
        }

       
    }
}