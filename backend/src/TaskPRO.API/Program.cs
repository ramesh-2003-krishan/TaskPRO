using TaskPRO.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using TaskPRO.Application.features.Projects.Services;
using TaskPRO.Application.features.Projects.Interfaces;
using TaskPRO.Application.common.interfaces;
using TaskPRO.Infrastructure.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.CodeAnalysis.Options;
using Microsoft.IdentityModel.Tokens.Experimental;
using TaskPRO.Application.features.SubTask.Validators;
using TaskPRO.Application.features.SubTask.Interfaces;
using TaskPRO.Application.features.SubTask.Service;
using TaskPRO.Infrastructure.Storage;

var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["Jwt:SecretKey"]
    ?? throw new InvalidOperationException("JWT configuration is missing: Jwt:SecretKey");

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Users.Validators.UpdateProfileRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Users.Validators.ChangePasswordRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Users.Validators.UpdateUserRoleRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Projects.Validators.CreateProjectValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Projects.Validators.AddProjectMemberValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Projects.Validators.UpdateProjectMemberRoleValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Projects.Validators.UpdateProjectValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Task.Validators.CreateTaskValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Task.Validators.UpdateTaskValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Task.Validators.AssignTaskValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.TaskComments.Validators.CreateCommentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.TaskComments.Validators.UpdateCommentValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectAuthorizationService, ProjectAuthorizationService>();
builder.Services.AddScoped<ISubTaskService, SubTaskService>();


var appSettingsSection = builder.Configuration.GetSection("AppSettings");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Users.Validators.UpdateProfileRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Users.Validators.ChangePasswordRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Users.Validators.UpdateUserRoleRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Projects.Validators.CreateProjectValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Projects.Validators.AddProjectMemberValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Projects.Validators.UpdateProjectMemberRoleValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Projects.Validators.UpdateProjectValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Task.Validators.CreateTaskValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Task.Validators.UpdateTaskValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Task.Validators.AssignTaskValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.TaskComments.Validators.CreateCommentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.TaskComments.Validators.UpdateCommentValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "TaskPRO API",
            Version = "v1"
        });

        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    }
);

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
    ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins(allowedOrigins)
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.Configure<LocalFileStorageService>(builder.Configuration.GetSection("FileStorageSettings"));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectAuthorizationService, ProjectAuthorizationService>();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(Option =>
{
    Option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(jwtKey))
    };
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

