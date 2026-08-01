using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskPRO.Domain.entities;
using TaskPRO.Domain.enums;

namespace TaskPRO.Infrastructure.configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.Name)
                .HasConversion<string>()
                .IsRequired();
            builder.HasData(
                new Role { Id = Guid.NewGuid(), Name = ProjectRole.Admin, Description = "Admin role with full access", CreatedAt = DateTime.UtcNow },
                new Role { Id = Guid.NewGuid(), Name = ProjectRole.ProjectManager, Description = "Project manager role", CreatedAt = DateTime.UtcNow },
                new Role { Id = Guid.NewGuid(), Name = ProjectRole.Member, Description = "Project member role", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}