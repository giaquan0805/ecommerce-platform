using ecommerce_platform.Domain.Enums;

namespace ecommerce_platform.Domain.Entities
{
    public class Product
    {
        public long Id { get; set; }

        public long ShopId { get; set; }

        public long CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Material { get; set; }

        public string? Gender { get; set; }

        public ProductStatus Status { get; set; } = ProductStatus.Draft;

        public bool IsTryOnEnabled { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public Shop? Shop { get; set; }

        public Category? Category { get; set; }

        public ICollection<ProductVariant> Variants { get; set; }
            = new List<ProductVariant>();

        public ICollection<ProductImage> Images { get; set; }
            = new List<ProductImage>();

        public ICollection<TryOnRequest> TryOnRequests { get; set; }
            = new List<TryOnRequest>();
    }
}