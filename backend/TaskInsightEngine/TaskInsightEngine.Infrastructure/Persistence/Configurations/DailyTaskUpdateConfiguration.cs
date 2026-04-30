using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class DailyTaskUpdateConfiguration : IEntityTypeConfiguration<DailyTaskUpdateStatus>
    {
        public void Configure(EntityTypeBuilder<DailyTaskUpdateStatus> builder)
        {
            builder.ToTable("dailyTaskUpdateStatus");

            builder.HasKey(dt => dt.DailyTaskUpdateStatusId);

            builder.HasOne<TaskItem>()
                   .WithMany()
                   .HasForeignKey(t => t.TaskId);

            builder.HasOne<Status>()
                   .WithMany()
                   .HasForeignKey(s => s.StatusId);

            builder.HasOne(pm => pm.ProjectMember)
                    .WithMany(dtu => dtu.DailyTaskUpdateStatus)
                    .HasForeignKey(pm => pm.ProjectMemberId);

            builder.HasOne(x => x.TaskItem)
                   .WithMany(t => t.DailyTaskUpdateStatuses)
                   .HasForeignKey(x => x.TaskId);

            builder.Property(c => c.CreatedDate)
                   .HasDefaultValueSql("NOW()");

            builder.Property(c => c.UpdatedDate)
                   .HasDefaultValueSql("NOW()");
        }
    }
}

