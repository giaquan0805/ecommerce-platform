using ecommerce_platform.Domain.Enums;

namespace ecommerce_platform.Domain.Entities
{
    public class OrderStatusHistory
    {
        public long Id { get; set; }

        public long OrderId { get; set; }

        public OrderStatus? OldStatus { get; set; }

        public OrderStatus NewStatus { get; set; }

        public string ChangedByUserId { get; set; } = string.Empty;

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public string? Note { get; set; }

        // Navigation Properties
        public Order? Order { get; set; }

        public ApplicationUser? ChangedByUser { get; set; }
    }
}