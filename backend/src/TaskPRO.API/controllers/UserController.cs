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
    }
}