using Microsoft.EntityFrameworkCore;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;

namespace TaskPRO.Infrastructure.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>()
            .Property(r => r.Name)
            .HasConversion<String>();

        modelBuilder.Entity<TaskItem>()
            .Property(t => t.Status)
            .HasConversion<String>();

        modelBuilder.Entity<User>()
            .Property(u => u.Name)
            .HasConversion<String>();

        modelBuilder.Entity<Project>()
            .Property(p => p.Role)
            .HasConversion<String>();

    }
}