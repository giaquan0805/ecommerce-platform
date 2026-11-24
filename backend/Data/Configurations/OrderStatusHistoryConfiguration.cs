using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class OrderStatusHistoryConfiguration
        : IEntityTypeConfiguration<OrderStatusHistory>
    {
        public void Configure(
            EntityTypeBuilder<OrderStatusHistory> builder)
        {
            builder.ToTable("OrderStatusHistory");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.OldStatus)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(h => h.NewStatus)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(h => h.ChangedAt)
                .IsRequired();

            builder.Property(h => h.Note)
                .HasMaxLength(500);

            // Order 1 - N OrderStatusHistory
            builder.HasOne(h => h.Order)
                .WithMany(o => o.StatusHistories)
                .HasForeignKey(h => h.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // ApplicationUser 1 - N OrderStatusHistory
            builder.HasOne(h => h.ChangedByUser)
                .WithMany(u => u.OrderStatusChanges)
                .HasForeignKey(h => h.ChangedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Hỗ trợ truy vấn lịch sử theo đơn hàng
            builder.HasIndex(h => new
            {
                h.OrderId,
                h.ChangedAt
            });
        }
    }
}