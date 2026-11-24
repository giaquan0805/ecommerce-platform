using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class ProductImageConfiguration
        : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Color)
                .HasMaxLength(50);

            builder.Property(i => i.ImageUrl)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(i => i.ImageType)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(i => i.IsPrimary)
                .HasDefaultValue(false);

            builder.Property(i => i.SortOrder)
                .HasDefaultValue(0);

            builder.Property(i => i.AltText)
                .HasMaxLength(255);

            // Ràng buộc dữ liệu
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_ProductImages_TryOnColor",
                    """
                    "ImageType" NOT IN ('TryOnFront', 'TryOnBack')
                    OR
                    (
                        "Color" IS NOT NULL
                        AND LENGTH(TRIM("Color")) > 0
                    )
                    """
                );
            });

            // Mỗi Product chỉ có tối đa một ảnh đại diện chính
            builder.HasIndex(i => i.ProductId)
                .IsUnique()
                .HasFilter("\"IsPrimary\" = TRUE");

            // Mỗi Product + Color chỉ có tối đa một ảnh TryOnFront
            builder.HasIndex(i => new { i.ProductId, i.Color })
                .IsUnique()
                .HasFilter("\"ImageType\" = 'TryOnFront'");

            // Quan hệ Product 1 - N ProductImages
            builder.HasOne(i => i.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}