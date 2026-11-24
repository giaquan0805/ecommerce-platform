using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class CheckoutConfiguration
        : IEntityTypeConfiguration<Checkout>
    {
        public void Configure(EntityTypeBuilder<Checkout> builder)
        {
            builder.ToTable("Checkouts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.ReceiverName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.ReceiverPhone)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(c => c.ShippingAddress)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(c => c.TotalAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(c => c.PaymentMethod)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            // Chống trùng yêu cầu Checkout
            builder.HasIndex(c => c.IdempotencyKey)
                .IsUnique();

            // Tổng tiền không được âm
            builder.ToTable(t =>
                t.HasCheckConstraint(
                    "CK_Checkouts_TotalAmount",
                    "\"TotalAmount\" >= 0"
                )
            );

            // User 1 - N Checkouts
            builder.HasOne(c => c.User)
                .WithMany(u => u.Checkouts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}