using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class RiskfactorConfiguration: IEntityTypeConfiguration<RiskFactor>
    {
        public void Configure(EntityTypeBuilder<RiskFactor> builder)
        {

        }
    }
}
