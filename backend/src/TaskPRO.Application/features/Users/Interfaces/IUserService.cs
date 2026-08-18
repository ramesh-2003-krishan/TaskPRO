using System;
using System.Threading.Tasks;
using TaskPRO.Application.features.Users.DTOs;

namespace TaskPRO.Application.features.Users.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetProfileAsync(Guid userId);
        Task<UserResponse> UpdateProfileAsync(Guid userId, UpdateProfileRequest updateUserDto);
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordRequest updatePasswordDto);
        Task<UserListResponse> GetUserAsync(UserListRequest userListRequest);
        Task<UserResponse> GetUserByIdAsync(Guid userId);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<bool> UpdateUserStatusAsync(Guid userId,  bool isActive);
        Task<bool> UpdateUserRoleAsync(Guid userId, string role);
        
        
    }
}