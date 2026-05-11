using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class TaskImpedimentCommentConfiguration :IEntityTypeConfiguration<TaskImpedimentComment>
    {
        public void Configure(EntityTypeBuilder<TaskImpedimentComment> builder)
        {
            builder.ToTable("taskimpedimentcomments");

            builder.HasKey(t=>t.TaskImpedimentCommentId);

            builder.HasOne(ti=>ti.TaskImpediment)
                    .WithMany(c=>c.TaskImpedimentComments)
                    .HasForeignKey(ti=>ti.TaskImpedimentId);

            builder.HasOne(pm => pm.Member)
                    .WithMany(c => c.TaskImpedimentComments)
                    .HasForeignKey(pm => pm.CreatedBy);

            builder.Property(tc => tc.CreatedAt).HasDefaultValueSql("NOW()");

        }
    }
}
