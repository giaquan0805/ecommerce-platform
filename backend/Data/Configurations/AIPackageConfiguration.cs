using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class AIPackageConfiguration
        : IEntityTypeConfiguration<AIPackage>
    {
        public void Configure(EntityTypeBuilder<AIPackage> builder)
        {
            builder.ToTable("AIPackages");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.TotalCredits)
                .IsRequired();

            builder.Property(p => p.DurationDays)
                .HasDefaultValue(30)
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            // Không cho phép trùng tên gói
            builder.HasIndex(p => p.Name)
                .IsUnique();

            // Ràng buộc dữ liệu
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_AIPackages_Price",
                    "\"Price\" >= 0"
                );

                t.HasCheckConstraint(
                    "CK_AIPackages_TotalCredits",
                    "\"TotalCredits\" > 0"
                );

                t.HasCheckConstraint(
                    "CK_AIPackages_DurationDays",
                    "\"DurationDays\" > 0"
                );
            });
        }
    }
}