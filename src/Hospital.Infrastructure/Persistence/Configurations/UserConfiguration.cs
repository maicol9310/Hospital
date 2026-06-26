using Hospital.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Username).IsRequired().HasMaxLength(200);
            builder.Property(o => o.PasswordHash).HasMaxLength(500);
            builder.Property(o => o.Role).IsRequired();
        }
    }
}
