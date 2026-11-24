using ecommerce_platform.Domain.Enums;

namespace ecommerce_platform.Domain.Entities
{
    public class Checkout
    {
        public long Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string ReceiverName { get; set; } = string.Empty;

        public string ReceiverPhone { get; set; } = string.Empty;

        public string ShippingAddress { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
            = PaymentMethod.COD;

        public Guid IdempotencyKey { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public ApplicationUser? User { get; set; }

        public ICollection<Order> Orders { get; set; }
            = new List<Order>();
    }
}