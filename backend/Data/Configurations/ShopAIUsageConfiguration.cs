using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class ShopAIUsageConfiguration
        : IEntityTypeConfiguration<ShopAIUsage>
    {
        public void Configure(EntityTypeBuilder<ShopAIUsage> builder)
        {
            builder.ToTable("ShopAIUsages");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UsedCount)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(u => u.ReservedCount)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            // Mỗi Subscription chỉ có một Usage
            builder.HasIndex(u => u.ShopAISubscriptionId)
                .IsUnique();

            // Không cho phép số lượt âm
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_ShopAIUsages_UsedCount",
                    "\"UsedCount\" >= 0"
                );

                t.HasCheckConstraint(
                    "CK_ShopAIUsages_ReservedCount",
                    "\"ReservedCount\" >= 0"
                );
            });

            // Subscription 1 - 1 Usage
            builder.HasOne(u => u.Subscription)
                .WithOne(s => s.Usage)
                .HasForeignKey<ShopAIUsage>(
                    u => u.ShopAISubscriptionId
                )
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}