using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;


namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure (EntityTypeBuilder<TaskItem>builder)
        {
            builder.ToTable("taskItem");
            builder.HasKey(t => t.TaskItemId);

            builder.HasOne<Priority>()
                   .WithMany()
                   .HasForeignKey(x => x.PriorityId);

            builder.HasOne<Project>()
                   .WithMany()
                   .HasForeignKey(x => x.ProjectId);

            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql("NOW()");

            builder.Property(u => u.UpdatedAt)
                    .HasDefaultValueSql("NOW()");
        }
    }
}
