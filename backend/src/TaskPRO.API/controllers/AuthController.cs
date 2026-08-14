using TaskPRO.Application.DTOs.LoginRequest;
using TaskPRO.Application.DTOs.RegisterRequest;
using TaskPRO.Application.DTOs.RefreshTokenRequest;
using TaskPRO.Application.DTOs.AuthResponse;
using TaskPRO.Application.DTOs.LogoutRequest;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using TaskPRO.Application.interfaces;
using TaskPRO.Infrastructure.Data;

namespace TaskPRO.API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenarator _jwtTokenService;

        public AuthController(AppDBContext context, IPasswordHasher passwordHasher, IJwtTokenGenarator jwtTokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == request.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already in use.");
            }

            var user = new User
            {
                Username = request.Username,
                UserEmail = request.Email,
                PasswordHashedValue = _passwordHasher.HashPassword(request.Password),
                
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == request.Email);
            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHashedValue))
            {
                return Unauthorized("Invalid email or password.");
            }

            var role = user.Name.ToString();
            var accessToken = _jwtTokenService.GenerateToken(user, role);
            var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Id);

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var existingToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);
            if (existingToken == null || existingToken.IsExpired || existingToken.Revoked != null)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }

            var user = await _context.Users.FindAsync(existingToken.UserId);
            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            var role = user.Name.ToString();
            var newAccessToken = _jwtTokenService.GenerateToken(user, role);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken(user.Id);

           
            existingToken.Token = newRefreshToken.Token; 
            await _context.SaveChangesAsync();

            return Ok(new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            var existingToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);
            if (existingToken != null)
            {
                _context.RefreshTokens.Remove(existingToken);
                await _context.SaveChangesAsync();
            }

            return Ok("Logged out successfully.");
        }


        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID claim not found.");
            }

            var userId = Guid.Parse(userIdClaim.Value);
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(new
            {
                user.Id,
                user.Username,
                user.UserEmail,
                Role = user.Name.ToString(),
                user.Description,
                user.CreatedAt
            });
        }
    }
}
