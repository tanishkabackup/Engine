using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class RiskSubscriptionConfiguration : IEntityTypeConfiguration<RiskSubscription>
    {
        public void Configure(EntityTypeBuilder<RiskSubscription> builder)
        {
            builder.ToTable("risksubscriptions");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserEmail);
            builder.HasIndex(x => new { x.UserEmail, x.ProjectId }).IsUnique();
        }
    }
}
