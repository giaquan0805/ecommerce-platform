using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class ProductVariantConfiguration
        : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Sku)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(v => v.Sku)
                .IsUnique();

            builder.Property(v => v.Size)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(v => v.Color)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(v => v.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(v => v.StockQuantity)
                .IsRequired();

            builder.Property(v => v.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            // Một Product không được trùng tổ hợp Size + Color
            builder.HasIndex(v => new
            {
                v.ProductId,
                v.Size,
                v.Color
            }).IsUnique();

            // Giá bán và tồn kho không được âm
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_ProductVariants_Price",
                    "\"Price\" >= 0"
                );

                t.HasCheckConstraint(
                    "CK_ProductVariants_StockQuantity",
                    "\"StockQuantity\" >= 0"
                );
            });

            // Quan hệ Product 1 - N ProductVariants
            builder.HasOne(v => v.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}