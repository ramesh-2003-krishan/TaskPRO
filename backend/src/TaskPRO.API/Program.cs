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

