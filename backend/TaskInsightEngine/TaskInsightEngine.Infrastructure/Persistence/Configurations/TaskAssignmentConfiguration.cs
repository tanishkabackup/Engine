using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class TaskAssignmentConfiguration : IEntityTypeConfiguration<TaskAssignment>
    {
        public void Configure(EntityTypeBuilder<TaskAssignment> builder)
        {
            builder.ToTable("taskassignments");

            builder.HasKey(ts => ts.TaskAssignmentId);

            builder.HasOne(ta => ta.TaskItem)
                   .WithMany(t => t.TaskAssignment)
                   .HasForeignKey(x => x.TaskItemId);

            builder.HasOne(ta => ta.AssigneeMember)
                   .WithMany()
                   .HasForeignKey(ta => ta.AssigneeId);

            builder.HasOne(ta => ta.AssignerMember)
                   .WithMany()
                   .HasForeignKey(ta => ta.AssignerId);

            builder.HasOne(ta => ta.Manager)
                   .WithMany()
                   .HasForeignKey(ta => ta.ManagerId);

        }
    }
}
