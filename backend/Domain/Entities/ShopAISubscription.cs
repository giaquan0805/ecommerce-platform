using ecommerce_platform.Domain.Enums;

namespace ecommerce_platform.Domain.Entities
{
    public class ShopAISubscription
    {
        public long Id { get; set; }

        public long ShopId { get; set; }

        public long AIPackageId { get; set; }

        // Snapshot thông tin gói tại thời điểm mua
        public string PackageName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int TotalCredits { get; set; }

        public int DurationDays { get; set; }

        public ShopAISubscriptionStatus Status { get; set; }
            = ShopAISubscriptionStatus.PendingPayment;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Shop? Shop { get; set; }

        public AIPackage? AIPackage { get; set; }

        public ShopAIUsage? Usage { get; set; }

        public AIPackagePayment? Payment { get; set; }
    }
}