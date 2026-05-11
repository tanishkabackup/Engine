using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class BriefingConfiguration : IEntityTypeConfiguration<BriefingEntry>
    {
        public void Configure(EntityTypeBuilder<BriefingEntry> builder)
        {
            builder.ToTable("briefings");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.CreatedAt)
                   .IsRequired();

            builder.HasIndex(b => b.CreatedAt);

            builder.HasIndex(b => b.AttentionCount);
            builder.HasIndex(b => b.SilentCount);
            builder.HasIndex(b => b.RecoveringCount);

            builder.OwnsOne(b => b.BriefingSnapshot, snapshotBuilder =>
            {
                snapshotBuilder.ToJson();
                snapshotBuilder.OwnsMany(s => s.NeedsAttention);
                snapshotBuilder.OwnsMany(s => s.SlientRisk);
                snapshotBuilder.OwnsMany(s => s.Recovering);
            });
        }
    }
}
