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
         public async Task<ProjectprojectRole?> GetUserRoleInProjectAsync(Guid projectId, Guid userId)
        {
            var project = await _dbContext.Projects
                .Include(p => p.ProjectMembers)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null) return null;

            var userRole = project.ProjectMembers
                .Where(pu => pu.UserId == userId)
                .Select(pu => (ProjectprojectRole?)pu.Role)
                .FirstOrDefault();

            return userRole;
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
            return role == ProjectprojectRole.Owner || role == ProjectprojectRole.Manager;
        }

        public async Task<bool> CanDeleteProjectAsync(Guid projectId, Guid userId, bool isAdmin)
        {
            if (isAdmin) return true;

            var role = await GetUserRoleInProjectAsync(projectId, userId);
            return role == ProjectprojectRole.Owner;
        }
        public async Task<ProjectMember?> GetProjectMemberAsync(Guid projectId, Guid userId)
        {
            var project = await _dbContext.Projects
                .Include(p => p.ProjectMembers)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null) return null;

            var Member = project.ProjectMembers
                .FirstOrDefault(pm => pm.UserId == userId);

            return Member;
        }

       
    }
}