using ecommerce_platform.Domain.Enums;

namespace ecommerce_platform.Domain.Entities
{
    public class AIPackagePayment
    {
        public long Id { get; set; }

        public long ShopAISubscriptionId { get; set; }

        public decimal Amount { get; set; }

        public AIPackagePaymentStatus Status { get; set; }
            = AIPackagePaymentStatus.Pending;

        public string? TransactionReference { get; set; }

        public string? ConfirmedByUserId { get; set; }

        public DateTime? PaidAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Note { get; set; }

        // Navigation Properties
        public ShopAISubscription? Subscription { get; set; }

        public ApplicationUser? ConfirmedByUser { get; set; }
    }
}