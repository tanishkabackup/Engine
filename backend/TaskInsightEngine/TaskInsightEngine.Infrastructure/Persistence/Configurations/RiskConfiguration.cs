using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class RiskConfiguration : IEntityTypeConfiguration<Risk>
    {
        public void Configure(EntityTypeBuilder<Risk> builder)
        {
               builder.ToTable("risk");
            
                builder.HasKey(r => r.RiskId);

                builder.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(r => r.Description)
                    .HasMaxLength(255);

                builder.Property(r => r.Weight)
                    .IsRequired();

                builder.HasData(
                    new Risk
                    {
                        RiskId = 1,
                        Name = "External Dependency",
                        Description = "Blocked by third-party/vendor or external system",
                        Weight = 3
                    },
                    new Risk
                    {
                        RiskId = 2,
                        Name = "Internal Blocker",
                        Description = "Blocked due to internal team dependency or issue",
                        Weight = 2
                    },
                    new Risk
                    {
                        RiskId = 3,
                        Name = "Resource Constraint",
                        Description = "Insufficient resources or bandwidth",
                        Weight = 3
                    },
                    new Risk
                    {
                        RiskId = 4,
                        Name = "Technical Uncertainty",
                        Description = "Unknown technical challenges or complexity",
                        Weight = 4
                    },
                    new Risk
                    {
                        RiskId = 5,
                        Name = "Requirement Gap",
                        Description = "Unclear or changing requirements",
                        Weight = 5
                    }
                );
            }
        }
    }

