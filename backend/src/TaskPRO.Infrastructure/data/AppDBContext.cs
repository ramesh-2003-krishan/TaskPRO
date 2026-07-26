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
    public DbSet<ProjectMember> ProjectMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>()
            .Property(r => r.Name)
            .HasConversion<String>();

        modelBuilder.Entity<TaskItem>()
            .Property(t => t.Status)
            .HasConversion<String>();

        modelBuilder.Entity<TaskItem>()
            .Property(t => t.Priority)
            .HasConversion<String>();

        modelBuilder.Entity<TaskItem>()
            .HasMany(t => t.Comments)
            .WithOne(c => c.TaskItem)
            .HasForeignKey(c => c.TaskItemId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskItem>()
            .HasMany<User>()
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "TaskItemUser",
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<TaskItem>().WithMany().HasForeignKey("TaskItemId").OnDelete(DeleteBehavior.Cascade)
            );

        modelBuilder.Entity<TaskItem>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .Property(u => u.Name)
            .HasConversion<String>();

        modelBuilder.Entity<Project>()
            .Property(p => p.Role)
            .HasConversion<String>();

        modelBuilder.Entity<ProjectMember>()
            .Property(pm => pm.Role)
            .HasConversion<String>();

    }
}