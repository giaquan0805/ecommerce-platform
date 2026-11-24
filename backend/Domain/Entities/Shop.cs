using ecommerce_platform.Domain.Enums;

namespace ecommerce_platform.Domain.Entities
{
    public class Shop
    {
        public long Id { get; set; }

        public string OwnerId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;    

        public string? Description { get; set; }

        public string? LogoUrl { get; set; }

        public string? BannerUrl { get; set; }

        public ShopStatus Status { get; set; } = ShopStatus.Pending;

        public string? RejectionReason { get; set; }

        public decimal DefaultShippingFee { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? ApprovedAt { get; set; }

        // Navigation Property
        public ApplicationUser? Owner { get; set; }

        public ICollection<Product> Products { get; set; }
            = new List<Product>();

        public ICollection<Order> Orders { get; set; }
            = new List<Order>();

        public ICollection<ShopAISubscription> AISubscriptions { get; set; }
            = new List<ShopAISubscription>();

        public ICollection<TryOnRequest> TryOnRequests { get; set; }
            = new List<TryOnRequest>();
    }
}