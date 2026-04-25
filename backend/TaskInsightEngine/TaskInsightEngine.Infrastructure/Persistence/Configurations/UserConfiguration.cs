using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.UserId);

            builder.HasOne(u => u.Role)
                   .WithMany(r => r.Users)
                   .HasForeignKey(u => u.RoleId);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("NOW()");

            builder.Property(c => c.UpdatedAt)
                   .HasDefaultValueSql("NOW()");
        }
    }
}
