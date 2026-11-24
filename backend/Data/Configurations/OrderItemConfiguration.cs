using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class OrderItemConfiguration
        : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.ProductName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(oi => oi.Sku)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(oi => oi.Size)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(oi => oi.Color)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(oi => oi.Quantity)
                .IsRequired();

            builder.Property(oi => oi.TotalPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            // Kiểm tra giá tiền và số lượng
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_OrderItems_UnitPrice",
                    "\"UnitPrice\" >= 0"
                );

                t.HasCheckConstraint(
                    "CK_OrderItems_Quantity",
                    "\"Quantity\" > 0"
                );

                t.HasCheckConstraint(
                    "CK_OrderItems_TotalPrice",
                    "\"TotalPrice\" = \"UnitPrice\" * \"Quantity\""
                );
            });

            // Order 1 - N OrderItems
            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProductVariant 1 - N OrderItems
            builder.HasOne(oi => oi.Variant)
                .WithMany(v => v.OrderItems)
                .HasForeignKey(oi => oi.VariantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}