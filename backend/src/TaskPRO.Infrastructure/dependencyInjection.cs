using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskPRO.Infrastructure.Data;
using TaskPRO.Infrastructure.authentication;
using TaskPRO.Application.interfaces;
using TaskPRO.Infrastructure.authentication.CurrentUserService;
using TaskPRO.Application.Features.Users.Interfaces;
using TaskPRO.Infrastructure.repositories;

namespace TaskPRO.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDBContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IJwtTokenGenarator, JwtTokenGenarator>();

        services.AddHttpContextAccessor();
        
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}