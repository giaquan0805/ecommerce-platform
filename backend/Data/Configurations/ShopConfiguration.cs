using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable("Shops");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasMaxLength(180)
                .IsRequired();

            builder.Property(s => s.Slug)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(s => s.Slug)
                .IsUnique();

            builder.Property(s => s.Description)
                .HasColumnType("text");

            builder.Property(s => s.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(s => s.RejectionReason)
                .HasMaxLength(500);

            builder.Property(s => s.DefaultShippingFee)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.ToTable(t =>
                t.HasCheckConstraint(
                    "CK_Shops_DefaultShippingFee",
                    "\"DefaultShippingFee\" >= 0"
                )
            );

            builder.HasOne(s => s.Owner)
                .WithMany(u => u.Shops)
                .HasForeignKey(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}