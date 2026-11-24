using Microsoft.AspNetCore.Identity;

namespace ecommerce_platform.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        public string? AvatarUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public ICollection<UserAddress> Addresses { get; set; }
            = new List<UserAddress>();

        public ICollection<Shop> Shops { get; set; }
            = new List<Shop>();

        public Cart? Cart { get; set; }

        public ICollection<Checkout> Checkouts { get; set; }
            = new List<Checkout>();

        public ICollection<OrderStatusHistory> OrderStatusChanges { get; set; }
            = new List<OrderStatusHistory>();

        public ICollection<TryOnRequest> TryOnRequests { get; set; }
            = new List<TryOnRequest>();

        public ICollection<AIPackagePayment> ConfirmedAIPackagePayments { get; set; } 
            = new List<AIPackagePayment>();
    }
}
