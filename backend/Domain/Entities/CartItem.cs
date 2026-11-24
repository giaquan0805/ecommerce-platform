namespace ecommerce_platform.Domain.Entities
{
    public class CartItem
    {
        public long Id { get; set; }

        public long CartId { get; set; }

        public long VariantId { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Cart? Cart { get; set; }

        public ProductVariant? Variant { get; set; }
    }
}