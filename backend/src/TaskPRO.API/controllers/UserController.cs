using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TaskPRO.Application.interfaces;
using TaskPRO.Application.features.Users.DTOs;
using TaskPRO.Application.features.Users.Interfaces;

namespace TaskPRO.API.controllers.UserController
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public UserController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserResponse>> GetMyProfile()
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                return Unauthorized();
            }

            var UserResponse = await _userService.GetUserByIdAsync(userId.Value);

            return Ok(UserResponse);
        }

        [HttpPut("me")]
        public async Task<ActionResult<UserResponse>> UpdateMyProfile([FromBody] UpdateProfileRequest request)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                return Unauthorized();
            }

            var updatedUser = await _userService.UpdateProfileAsync(userId.Value, request);

            return Ok(updatedUser);
        }

        [HttpDelete("me")]
        public async Task<ActionResult> DeleteMyAccount()
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                return Unauthorized();
            }

            await _userService.DeleteUserAsync(userId.Value);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{id}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponse>> UpdateUserRole( Guid id,[FromBody] UpdateUserRoleRequest request)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid user ID.");
            }
            var updatedUser = await _userService.UpdateUserRoleAsync(id, request.Role);

            return Ok(updatedUser);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponse>> UpdateUserStatus(Guid id, [FromBody] bool IsActive)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid user ID.");
            }

            var updatedUser = await _userService.UpdateUserStatusAsync(id, IsActive);

            return Ok(updatedUser);
        }

        [HttpGet("profile")]
        public async Task<ActionResult<UserResponse>> GetProfile()
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByIdAsync(userId.Value);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("profile")]
        public async Task<ActionResult<UserResponse>> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                return Unauthorized();
            }

            var updatedUser = await _userService.UpdateProfileAsync(userId.Value, request);

            return Ok(updatedUser);
        }

    }
}