namespace ecommerce_platform.Domain.Entities
{
    public class AIPackage
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int TotalCredits { get; set; }

        public int DurationDays { get; set; } = 30;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Property
        public ICollection<ShopAISubscription> Subscriptions { get; set; }
            = new List<ShopAISubscription>();
    }
}