using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskPRO.Infrastructure.Data;
using TaskPRO.Infrastructure.authentication;
using TaskPRO.Application.interfaces;

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

        return services;
    }
}