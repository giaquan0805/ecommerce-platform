namespace ecommerce_platform.Domain.Entities
{
    public class ShopAIUsage
    {
        public long Id { get; set; }

        public long ShopAISubscriptionId { get; set; }

        public int UsedCount { get; set; } = 0;

        public int ReservedCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Property
        public ShopAISubscription? Subscription { get; set; }

        public ICollection<TryOnRequest> TryOnRequests { get; set; }
            = new List<TryOnRequest>();
    }
}