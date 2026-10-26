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
          
        }
    }
}