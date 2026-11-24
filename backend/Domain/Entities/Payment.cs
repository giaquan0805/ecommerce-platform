using ecommerce_platform.Domain.Enums;

namespace ecommerce_platform.Domain.Entities
{
    public class Payment
    {
        public long Id { get; set; }

        public long OrderId { get; set; }

        public PaymentMethod Method { get; set; } = PaymentMethod.COD;

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public DateTime? PaidAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Order? Order { get; set; }
    }
}