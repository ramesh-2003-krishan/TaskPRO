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
      
    }
}