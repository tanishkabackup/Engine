using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;
using TaskInsightEngine.Domain.Enums;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {

        public void Configure(EntityTypeBuilder<Role>builder)
        {
            builder.ToTable("roles");

            builder.HasKey(r => r.RoleId);

            builder.HasData(
            new Role { RoleId = 1, Name = RoleTypes.Developer },
            new Role { RoleId = 2, Name = RoleTypes.ProjectManager }

           
        );
        }
    }
}
