using Hospital.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedNever();
            builder.Property(o => o.PatientId).IsRequired().HasMaxLength(50);
            builder.Property(o => o.PatientName).HasMaxLength(200);
            builder.Property(o => o.ServiceCode).IsRequired().HasMaxLength(50);
            builder.Property(o => o.ServiceDescription).HasMaxLength(200);
            builder.Property(o => o.Priority).IsRequired().HasConversion<string>().HasMaxLength(20);
            builder.Property(o => o.Status).IsRequired().HasConversion<string>().HasMaxLength(20);
            builder.Property(o => o.CreatedAt).IsRequired();
            builder.Property(o => o.ProcessedAt);
        }
    }
}