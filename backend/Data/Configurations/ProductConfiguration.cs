using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Slug)
                .HasMaxLength(220)
                .IsRequired();

            // Slug chỉ cần duy nhất trong cùng một Shop
            builder.HasIndex(p => new { p.ShopId, p.Slug })
                .IsUnique();

            builder.Property(p => p.Description)
                .HasColumnType("text");

            builder.Property(p => p.Material)
                .HasMaxLength(100);

            builder.Property(p => p.Gender)
                .HasMaxLength(30);

            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.IsTryOnEnabled)
                .HasDefaultValue(false);

            // Product thuộc một Shop
            builder.HasOne(p => p.Shop)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.ShopId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product thuộc một Category
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}