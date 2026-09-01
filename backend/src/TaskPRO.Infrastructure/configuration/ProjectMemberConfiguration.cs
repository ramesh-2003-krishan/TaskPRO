using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TaskPRO.Domain.entities;

namespace TaskPRO.Infrastructure.configuration
{
    public class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
    {
        public void Configure(EntityTypeBuilder<ProjectMember> builder)
        {

            builder.HasKey(Pm => Pm.Id);

            builder.HasIndex(x => new
            {
                x.ProjectId,
                x.UserId
            }).IsUnique();

            builder.HasOne(Pm => Pm.Project)
            .WithMany(p => p.ProjectMembers)
            .HasForeignKey(Pm => Pm.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(Pm => Pm.Project)
            .WithMany(u => u.ProjectMembers)
            .HasForeignKey(Pm => Pm.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);


        }
    }
}