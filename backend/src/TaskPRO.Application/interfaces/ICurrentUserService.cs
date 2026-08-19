using System;

namespace TaskPRO.Application.interfaces
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? UserEmail { get; }
        string? UserRole { get; }
    }
}