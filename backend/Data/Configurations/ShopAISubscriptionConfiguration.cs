using ecommerce_platform.Domain.Entities;
using ecommerce_platform.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class ShopAISubscriptionConfiguration
        : IEntityTypeConfiguration<ShopAISubscription>
    {
        public void Configure(
            EntityTypeBuilder<ShopAISubscription> builder)
        {
            builder.ToTable("ShopAISubscriptions");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.PackageName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(s => s.TotalCredits)
                .IsRequired();

            builder.Property(s => s.DurationDays)
                .IsRequired();

            builder.Property(s => s.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            // Mỗi Shop chỉ có tối đa một gói Active
            builder.HasIndex(s => s.ShopId)
                .IsUnique()
                .HasFilter("\"Status\" = 'Active'");

            // Ràng buộc dữ liệu
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_ShopAISubscriptions_Price",
                    "\"Price\" >= 0"
                );

                t.HasCheckConstraint(
                    "CK_ShopAISubscriptions_TotalCredits",
                    "\"TotalCredits\" > 0"
                );

                t.HasCheckConstraint(
                    "CK_ShopAISubscriptions_DurationDays",
                    "\"DurationDays\" > 0"
                );

                t.HasCheckConstraint(
                    "CK_ShopAISubscriptions_Dates",
                    """
                    (
                        "StartDate" IS NULL AND "EndDate" IS NULL
                    )
                    OR
                    (
                        "StartDate" IS NOT NULL
                        AND "EndDate" IS NOT NULL
                        AND "EndDate" > "StartDate"
                    )
                    """
                );

                t.HasCheckConstraint(
                    "CK_ShopAISubscriptions_StatusDates",
                    """
                    (
                        "Status" IN ('PendingPayment', 'Cancelled')
                        AND "StartDate" IS NULL
                        AND "EndDate" IS NULL
                    )
                    OR 
                    (
                        "Status" IN ('Active', 'Expired', 'Exhausted')
                        AND "StartDate" IS NOT NULL
                        AND "EndDate" IS NOT NULL
                    )
                    """
                );
            });

            // Shop 1 - N Subscriptions
            builder.HasOne(s => s.Shop)
                .WithMany(sh => sh.AISubscriptions)
                .HasForeignKey(s => s.ShopId)
                .OnDelete(DeleteBehavior.Restrict);

            // AIPackage 1 - N Subscriptions
            builder.HasOne(s => s.AIPackage)
                .WithMany(p => p.Subscriptions)
                .HasForeignKey(s => s.AIPackageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}