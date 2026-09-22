using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPRO.Domain.entities;

namespace TaskPRO.Application.Features.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid userId);
        Task<User> GetUserEmailByIdAsync(string UserEmail);
        Task<User> GetUserPasswordAsync(string CurrentPassword);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid userId);
        Task UserLoginRequestAsync (string CurrentPassword, string UserEmail);
    }
}