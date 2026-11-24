using ecommerce_platform.Domain.Enums;

namespace ecommerce_platform.Domain.Entities
{
    public class Order
    {
        public long Id { get; set; }

        public long CheckoutId { get; set; }

        public long ShopId { get; set; }

        public decimal Subtotal { get; set; }

        public decimal ShippingFee { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }
            = OrderStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? CancelledAt { get; set; }

        public string? CancellationReason { get; set; }

        // Navigation Properties
        public Checkout? Checkout { get; set; }

        public Shop? Shop { get; set; }

        public ICollection<OrderItem> Items { get; set; }
            = new List<OrderItem>();

        public ICollection<OrderStatusHistory> StatusHistories { get; set; }
            = new List<OrderStatusHistory>();

        public Payment? Payment { get; set; }
    }
}