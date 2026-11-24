using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class CartItemConfiguration
        : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Quantity)
                .IsRequired();

            // Không được trùng Variant trong cùng một Cart
            builder.HasIndex(ci => new
            {
                ci.CartId,
                ci.VariantId
            }).IsUnique();

            // Quantity phải lớn hơn 0
            builder.ToTable(t =>
                t.HasCheckConstraint(
                    "CK_CartItems_Quantity",
                    "\"Quantity\" > 0"
                )
            );

            // Cart 1 - N CartItems
            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductVariant 1 - N CartItems
            builder.HasOne(ci => ci.Variant)
                .WithMany(v => v.CartItems)
                .HasForeignKey(ci => ci.VariantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}