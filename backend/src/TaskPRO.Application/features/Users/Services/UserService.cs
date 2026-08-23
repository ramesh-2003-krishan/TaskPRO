using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPRO.Application.features.Users.DTOs;
using TaskPRO.Application.features.Users.Interfaces;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;
using Microsoft.EntityFrameworkCore;



namespace TaskPRO.Application.features.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IAppDbContext _dbContext;

        public UserService(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserResponse> GetProfileAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            return MapToUserResponse(user);
        }

        public async Task<UserResponse> GetUserByIdAsync(Guid userId, UpdateProfileRequest UpdateUserDto)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

           user.Username = UpdateUserDto.Username;
           user.UserEmail = UpdateUserDto.Email;
           
            return MapToUserResponse(user);

        }

        public async Task<bool> UpdateUserStatusAsync(Guid userId, bool isActive)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            user.IsActive = isActive;
            await _dbContext.SaveChangesAsync(default);

            return true;
        }

        public async Task<bool> UpdateUserRoleAsync(Guid userId, string role)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            if (!Enum.TryParse(role, out ProjectUserRole parsedRole))
            {
                throw new ArgumentException($"Invalid role: {role}");
            }

            user.Name = parsedRole;
            await _dbContext.SaveChangesAsync(default);

            return true;
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordRequest updatePasswordDto)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

          
            user.PasswordHashedValue = updatePasswordDto.NewPassword; 

            await _dbContext.SaveChangesAsync(default);

            return true;
        }

        public async Task<UserListResponse> GetUserAsync(UserListRequest userListRequest)
        {
            var users = await _dbContext.Users.ToListAsync();

            var userResponses = new List<UserResponse>();

            foreach (var user in users)
            {
                userResponses.Add(MapToUserResponse(user));
            }

            return new UserListResponse
            {
                Users = userResponses
            };
        }

        public async Task<UserResponse> UpdateProfileAsync(Guid userId, UpdateProfileRequest updateUserDto)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            user.Username = updateUserDto.Username;
            user.UserEmail = updateUserDto.Email;

            await _dbContext.SaveChangesAsync(default);

            return MapToUserResponse(user);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(default);

            return true;
        }

        public async Task<UserResponse> GetUserByIdAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            return MapToUserResponse(user);
        }

        private static UserResponse MapToUserResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.UserEmail
            };
        }
    }
}