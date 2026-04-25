using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
    {
        public void Configure(EntityTypeBuilder<ProjectMember> builder)
        {
            builder.ToTable("projectmembers");

            builder.HasKey(pm => pm.ProjectMemberId);

            builder.HasOne(u => u.User)
                   .WithMany(r => r.Members)
                   .HasForeignKey(u => u.UserId);

            builder.HasOne<Project>()
                   .WithMany()
                   .HasForeignKey(pm => pm.ProjectId);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("NOW()");

            builder.Property(c => c.UpdatedAt)
                   .HasDefaultValueSql("NOW()");
        }
    }
}
