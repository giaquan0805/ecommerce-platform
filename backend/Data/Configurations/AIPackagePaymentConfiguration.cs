using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class AIPackagePaymentConfiguration
        : IEntityTypeConfiguration<AIPackagePayment>
    {
        public void Configure(
            EntityTypeBuilder<AIPackagePayment> builder)
        {
            builder.ToTable("AIPackagePayments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.TransactionReference)
                .HasMaxLength(150);

            builder.Property(p => p.Note)
                .HasMaxLength(1000);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            // Mỗi Subscription chỉ có một Payment
            builder.HasIndex(p => p.ShopAISubscriptionId)
                .IsUnique();

            // Không cho phép xác nhận trùng giao dịch ngân hàng
            builder.HasIndex(p => p.TransactionReference)
                .IsUnique()
                .HasFilter(
                    """
                    "Status" = 'Paid'
                    AND "TransactionReference" IS NOT NULL
                    """
                );

            // Không cho phép số tiền âm
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_AIPackagePayments_Amount",
                    "\"Amount\" >= 0"
                );

                t.HasCheckConstraint(
                    "CK_AIPackagePayments_StatusPaidAt",
                    """
                    (
                        "Status" = 'Paid'
                        AND "PaidAt" IS NOT NULL
                        AND "ConfirmedByUserId" IS NOT NULL
                    )
                    OR
                    (
                        "Status" IN ('Pending', 'Rejected', 'Cancelled')
                        AND "PaidAt" IS NULL
                    )
                    """
                );

                t.HasCheckConstraint(
                    "CK_AIPackagePayments_TransactionReference",
                    """
                    "Status" <> 'Paid'
                    OR "Amount" = 0
                    OR
                    (
                        "TransactionReference" IS NOT NULL
                        AND LENGTH(TRIM("TransactionReference")) > 0
                    )
                    """
                );
            });

            // Subscription 1 - 1 Payment
            builder.HasOne(p => p.Subscription)
                .WithOne(s => s.Payment)
                .HasForeignKey<AIPackagePayment>(
                    p => p.ShopAISubscriptionId
                )
                .OnDelete(DeleteBehavior.Restrict);

            // Admin 1 - N Payments
            builder.HasOne(p => p.ConfirmedByUser)
                .WithMany(u => u.ConfirmedAIPackagePayments)
                .HasForeignKey(p => p.ConfirmedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}