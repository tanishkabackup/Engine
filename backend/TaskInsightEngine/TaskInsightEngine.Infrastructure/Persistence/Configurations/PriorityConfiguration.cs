using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;
using TaskInsightEngine.Domain.Enums;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class PriorityConfiguration : IEntityTypeConfiguration<Priority>
    {

        public void Configure(EntityTypeBuilder<Priority> builder)
        {
            builder.ToTable("prioritys");

            builder.HasKey(r => r.PriorityId);

            builder.HasData(
             new Priority { PriorityId = 1, Type = PriorityTypes.Low },
             new Priority { PriorityId = 2, Type = PriorityTypes.Medium },
             new Priority { PriorityId = 3, Type = PriorityTypes.High }
         );

        }
    }
}
