using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class TryOnRequestConfiguration
        : IEntityTypeConfiguration<TryOnRequest>
    {
        public void Configure(EntityTypeBuilder<TryOnRequest> builder)
        {
            builder.ToTable("TryOnRequests");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.SelectedColor)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.InputImageUrl)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(r => r.GarmentImageUrl)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(r => r.OutputImageUrl)
                .HasColumnType("text");

            builder.Property(r => r.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(r => r.QuotaStatus)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(r => r.ErrorMessage)
                .HasMaxLength(1000);

            builder.Property(r => r.ConsentGiven)
                .IsRequired();

            builder.Property(r => r.RetryCount)
                .HasDefaultValue(0);

            // Mỗi RequestKey chỉ được sử dụng một lần
            builder.HasIndex(r => r.RequestKey)
                .IsUnique();

            builder.HasIndex(r => new
            {
                r.UserId,
                r.CreatedAt
            });

            builder.HasIndex(r => new
            {
                r.ShopId,
                r.CreatedAt
            });

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_TryOnRequests_RetryCount",
                    "\"RetryCount\" >= 0"
                );

                t.HasCheckConstraint(
                    "CK_TryOnRequests_Consent",
                    "\"ConsentGiven\" = TRUE AND \"ConsentAt\" IS NOT NULL"
                );
            });

            // Customer 1 - N TryOnRequests
            builder.HasOne(r => r.User)
                .WithMany(u => u.TryOnRequests)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Shop 1 - N TryOnRequests
            builder.HasOne(r => r.Shop)
                .WithMany(s => s.TryOnRequests)
                .HasForeignKey(r => r.ShopId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product 1 - N TryOnRequests
            builder.HasOne(r => r.Product)
                .WithMany(p => p.TryOnRequests)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProductImage 1 - N TryOnRequests
            builder.HasOne(r => r.ProductImage)
                .WithMany(i => i.TryOnRequests)
                .HasForeignKey(r => r.ProductImageId)
                .OnDelete(DeleteBehavior.Restrict);

            // AIModel 1 - N TryOnRequests
            builder.HasOne(r => r.AIModel)
                .WithMany(m => m.TryOnRequests)
                .HasForeignKey(r => r.AIModelId)
                .OnDelete(DeleteBehavior.Restrict);

            // ShopAIUsage 1 - N TryOnRequests
            builder.HasOne(r => r.ShopAIUsage)
                .WithMany(u => u.TryOnRequests)
                .HasForeignKey(r => r.ShopAIUsageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}