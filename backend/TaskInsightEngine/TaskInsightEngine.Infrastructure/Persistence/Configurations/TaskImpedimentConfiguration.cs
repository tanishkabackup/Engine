using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class TaskImpedimentConfiguration : IEntityTypeConfiguration<TaskImpediment>
    {
        public void Configure(EntityTypeBuilder<TaskImpediment> builder)
        {
            builder.ToTable("taskimpediments");

            builder.HasKey(ti => ti.TaskImpedimentId);

            builder.HasOne(r=>r.Risk)
                .WithMany()
                .HasForeignKey(x => x.RiskId);

            builder.HasOne(t => t.TaskItem)
                   .WithMany(ti => ti.TaskImpediments)
                   .HasForeignKey(t => t.TaskItemId);

            builder.HasMany(ta => ta.TaskAssignments)
                   .WithMany(ti => ti.TaskImpediments);
                   

            builder.Property(ti => ti.CreatedAt).HasDefaultValueSql("NOW()");

            builder.Property(ti => ti.UpdatedAt).HasDefaultValueSql("NOW()");

        }
    }
}
