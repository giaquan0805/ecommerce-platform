namespace ecommerce_platform.Domain.Entities
{
    public class Cart
    {
        public long Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public ApplicationUser? User { get; set; }

        public ICollection<CartItem> Items { get; set; }
            = new List<CartItem>();
    }
}