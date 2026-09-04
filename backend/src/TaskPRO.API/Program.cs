using TaskPRO.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using TaskPRO.Application.features.Projects.Services;
using TaskPRO.Application.features.Projects.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Users.Validators.UpdateProfileRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Users.Validators.ChangePasswordRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskPRO.Application.features.Users.Validators.UpdateUserRoleRequestValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IProjectService, ProjectService>();

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

