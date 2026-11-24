using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class PaymentConfiguration
        : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Method)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            // Mỗi Order chỉ có một Payment
            builder.HasIndex(p => p.OrderId)
                .IsUnique();

            // Ràng buộc số tiền
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Payments_Amount",
                    "\"Amount\" >= 0"
                );

                t.HasCheckConstraint(
                    "CK_Payments_StatusPaidAt",
                    """
                (
                    "Status" = 'Paid'
                    AND "PaidAt" IS NOT NULL
                )
                OR
                (
                    "Status" IN ('Pending', 'Failed', 'Cancelled')
                    AND "PaidAt" IS NULL
                )
                """
                );
            });

            // Order 1 - 1 Payment
            builder.HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}