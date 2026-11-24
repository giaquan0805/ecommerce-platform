using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Subtotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(o => o.ShippingFee)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(o => o.TotalAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(o => o.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(o => o.CancellationReason)
                .HasMaxLength(500);

            // Một Checkout chỉ có một Order cho mỗi Shop
            builder.HasIndex(o => new
            {
                o.CheckoutId,
                o.ShopId
            }).IsUnique();

            // Các khoản tiền không được âm
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Orders_Subtotal",
                    "\"Subtotal\" >= 0"
                );

                t.HasCheckConstraint(
                    "CK_Orders_ShippingFee",
                    "\"ShippingFee\" >= 0"
                );

                t.HasCheckConstraint(
                    "CK_Orders_TotalAmount",
                    "\"TotalAmount\" = \"Subtotal\" + \"ShippingFee\""
                );
            });

            // Checkout 1 - N Orders
            builder.HasOne(o => o.Checkout)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CheckoutId)
                .OnDelete(DeleteBehavior.Restrict);

            // Shop 1 - N Orders
            builder.HasOne(o => o.Shop)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.ShopId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}