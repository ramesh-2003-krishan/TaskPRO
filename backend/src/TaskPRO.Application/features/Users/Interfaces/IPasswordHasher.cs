using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskPRO.Domain.entities;

namespace TaskPRO.Application.features.Users.DTOs
{
    public interface IPasswordHasher
    {
       Task<bool> GetUserPassword(string password);
    }
}