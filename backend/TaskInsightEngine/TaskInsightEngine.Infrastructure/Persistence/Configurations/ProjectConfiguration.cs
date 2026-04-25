using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.ProjectId);
            builder.ToTable("projects");

            builder.HasOne<Priority>()
                   .WithMany()
                   .HasForeignKey(x => x.PriorityId);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("NOW()");

            builder.Property(c => c.UpdatedAt)
                   .HasDefaultValueSql("NOW()");
        }
    }
}
