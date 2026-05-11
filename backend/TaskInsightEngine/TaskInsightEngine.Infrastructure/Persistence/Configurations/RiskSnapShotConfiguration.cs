using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class RiskSnapShotConfiguration : IEntityTypeConfiguration<RiskSnapshot>
    {
        public void Configure(EntityTypeBuilder<RiskSnapshot>builder)
        {
            builder.ToTable("risksnapshots");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.HasOne(r => r.TaskItem).WithMany(t=>t.RiskSnapshots).HasForeignKey(r => r.TaskId);

            builder.HasIndex(r => new { r.TaskId, r.Date, r.Id })
                   .HasDatabaseName("IX_RiskSnapshot_TaskId_Date_Id")
                   .IsDescending(false, true, true);

        }
    }
}
