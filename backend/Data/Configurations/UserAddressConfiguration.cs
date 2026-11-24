using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class UserAddressConfiguration
        : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("UserAddresses");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.ReceiverName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(a => a.ReceiverPhone)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(a => a.Province)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Ward)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.AddressLine)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Mỗi User chỉ có tối đa một địa chỉ mặc định
            builder.HasIndex(a => a.UserId)
                .IsUnique()
                .HasFilter("\"IsDefault\" = TRUE");
        }
    }
}