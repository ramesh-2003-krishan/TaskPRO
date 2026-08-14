using TaskPRO.Domain.entities;

namespace TaskPRO.Application.interfaces
{
    public interface IJwtTokenGenarator
    {
        string GenerateToken(User user, string roleName);

        RefreshToken GenerateRefreshToken(Guid userId);
    }


}