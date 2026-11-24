using ecommerce_platform.Domain.Enums;

namespace ecommerce_platform.Domain.Entities
{
    public class ProductImage
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public string? Color { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public ProductImageType ImageType { get; set; }
            = ProductImageType.Gallery;

        public bool IsPrimary { get; set; } = false;

        public int SortOrder { get; set; } = 0;

        public string? AltText { get; set; }

        // Navigation Property
        public Product? Product { get; set; }

        public ICollection<TryOnRequest> TryOnRequests { get; set; }
            = new List<TryOnRequest>();
    }
}