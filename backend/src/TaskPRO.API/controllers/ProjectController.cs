using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPRO.Application.features.Projects.DTOs;
using TaskPRO.Application.features.Projects.Interfaces;
using TaskPRO.Application.interfaces;
using TaskPRO.Domain.enums;
using TaskPRO.Application.features.Projects.Services;


namespace TaskPRO.API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;

        public ProjectController(IProjectService projectService, ICurrentUserService currentUserService)
        {
            _projectService = projectService;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<ActionResult<ProjectResponse>> CreateProject([FromBody] CreateProjectRequest request)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var response = await _projectService.CreateProjectAsync(currentUserId.Value, request);

            return CreatedAtAction(nameof(CreateProject), new { id = response.Id }, response);
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectDetailResponse>> GetProjectById(Guid projectId)
        {
            var response = await _projectService.GetProjectByIdAsync(projectId);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("{projectId}/my-details")]
        public async Task<ActionResult<MyProjectDetailResponse>> GetMyProjectDetailBy(Guid projectId)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var response = await _projectService.GetMyProjectDetailByAsync(projectId, currentUserId.Value);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectResponse>>> SearchProjects([FromQuery] string? searchTerm, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var response = await _projectService.SearchProjectsAsync(currentUserId.Value, searchTerm, pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ProjectResponse>>> FilterProjects([FromQuery] string? filterBy, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var response = await _projectService.FilterProjectsAsync(currentUserId.Value, filterBy, pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("paginate")]
        public async Task<ActionResult<IEnumerable<ProjectResponse>>> PaginateProjects([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var response = await _projectService.PaginateProjectsAsync(currentUserId.Value, pageNumber, pageSize);
            return Ok(response);
        }
        [HttpGet("{projectId}/members/{userId}")]
        public async Task<ActionResult<ProjectMemberResponse>> GetProjectMemberById(Guid projectId, Guid userId)
        {
            var response = await _projectService.GetProjectMemberByIdAsync(projectId, userId);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPut("{projectId}")]
        [Authorize(Roles = "Admin, Owner, Manager")]
        public async Task<ActionResult<ProjectResponse>> UpdateProject(Guid projectId, [FromBody] UpdateProjectRequest request)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var response = await _projectService.UpdateProjectAsync(currentUserId.Value, projectId, request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("{projectId}/members")]
        public async Task<ActionResult<ProjectMemberResponse>> AddProjectMember(Guid projectId, [FromBody] AddProjectMemberRequest request)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return Unauthorized();
            }
            if (request.Role != ProjectRole.Admin  && request.Role != ProjectRole.ProjectManager)
            {
                return BadRequest("Invalid role. Role must be one of the following: Admin, Owner, Manager, Member.");
            }

            var response = await _projectService.AddProjectMemberAsync(projectId, request.UserId, request.Role.ToString());
            try
            {
                return CreatedAtAction(nameof(GetProjectMemberById), new { projectId = projectId, userId = response.UserId }, response);
            }
            catch (TaskPRO.Application.common.Exceptions.ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the project member: {ex.Message}");
            }
        }

        [HttpPut("{projectId}/members/{userId}")]
        public async Task<ActionResult<ProjectMemberResponse>> UpdateProjectMemberRole(Guid projectId, Guid userId, Guid targetUserId, [FromBody] UpdateProjectRoleRequest request)
        {
            var project = await _projectService.GetProjectByIdAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }
            if(project.UserId == targetUserId)
            {
                return BadRequest("You cannot change your own role in the project.");
            }
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var response = await _projectService.UpdateProjectMemberRoleAsync(projectId, userId, request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("{projectId}/members/{userId}")]
        public async Task<ActionResult> RemoveProjectMember(Guid projectId, Guid userId, Guid targetUserId)
        {
            var project = await _projectService.GetProjectByIdAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }
            if(project.UserId == targetUserId)
            {
                return BadRequest("You cannot remove yourself from the project.");
            }
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return Unauthorized();
            }
            await _projectService.RemoveProjectMemberAsync(projectId, userId);
            return NoContent();
        }

        [HttpPatch("{projectId}/archive")]
        public async Task<ActionResult<ProjectResponse>> ArchiveProject(Guid projectId)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var response = await _projectService.ArchiveProjectAsync(currentUserId.Value, projectId);
            return Ok(response);
        }

        [HttpDelete("{projectId}")]
        public async Task<ActionResult<ProjectResponse>> DeleteProject(Guid projectId)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var response = await _projectService.DeleteProjectAsync(currentUserId.Value, projectId);
            return Ok(response);
        }
    }
}