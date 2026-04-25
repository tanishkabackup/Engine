using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;
using TaskInsightEngine.Domain.Enums;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("status");
            builder.HasKey(s => s.StatusId);

            builder.HasData(
                  new Status { StatusId = 1, Type = StatusTypes.New },
                  new Status { StatusId = 2, Type = StatusTypes.Completed },
                  new Status { StatusId = 3, Type = StatusTypes.InProgress },
                  new Status { StatusId = 4, Type = StatusTypes.OnHold },
                  new Status { StatusId = 5, Type = StatusTypes.InReview},
                  new Status { StatusId = 6, Type = StatusTypes.Blocked }
                );
        }
    }
}
