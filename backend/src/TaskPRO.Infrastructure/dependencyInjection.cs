using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskPRO.Infrastructure.Data;
using TaskPRO.Infrastructure.authentication;
using TaskPRO.Application.interfaces;
using TaskPRO.Application.features.Task.Interfaces;
using TaskPRO.Application.features.Task.Services;
using TaskPRO.Infrastructure.authentication.CurrentUserService;
using TaskPRO.Application.Features.Users.Interfaces;
using TaskPRO.Infrastructure.repositories;
using TaskPRO.Application.features.TaskComments.Interfaces;
using TaskPRO.Application.features.TaskComments.Services;
using TaskPRO.Application.features.Projects.Interfaces;
using TaskPRO.Application.features.Users.Interfaces;
using TaskPRO.Application.features.Users.Services;
using TaskPRO.Application.features.SubTask.Interfaces;
using TaskPRO.Application.features.SubTask.Service;

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
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskService, TaskServices>();
        services.AddScoped<TaskPRO.Application.features.Projects.Interfaces.IProjectRepository, TaskPRO.Infrastructure.repositories.ProjectRepository>();
        services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
        services.AddScoped<ITaskCommentService, TaskCommentService>();
        services.AddScoped<ITaskService, TaskServices>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<TaskPRO.Application.features.Projects.Interfaces.IProjectRepository, TaskPRO.Infrastructure.repositories.ProjectRepository>();
        services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
        services.AddScoped<ITaskCommentService, TaskCommentService>();
        services.AddScoped<ITaskService, TaskServices>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<TaskPRO.Application.features.Projects.Interfaces.IProjectRepository, TaskPRO.Infrastructure.repositories.ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISubTaskRepository, SubTaskRepository>();
        services.AddScoped<ISubTaskService, SubTaskService>();

        return services;
    }
}