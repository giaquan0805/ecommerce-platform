using ecommerce_platform.Domain.Enums;

namespace ecommerce_platform.Domain.Entities
{
    public class TryOnRequest
    {
        public long Id { get; set; }

        // Customer và Shop
        public string UserId { get; set; } = string.Empty;

        public long ShopId { get; set; }

        // Sản phẩm được thử
        public long ProductId { get; set; }

        public long ProductImageId { get; set; }

        public string SelectedColor { get; set; } = string.Empty;

        // Model và lượt AI
        public long AIModelId { get; set; }

        public long ShopAIUsageId { get; set; }

        // Ảnh
        public string InputImageUrl { get; set; } = string.Empty;

        public string GarmentImageUrl { get; set; } = string.Empty;

        public string? OutputImageUrl { get; set; }

        // Trạng thái
        public TryOnRequestStatus Status { get; set; }
            = TryOnRequestStatus.Pending;

        public AIQuotaStatus QuotaStatus { get; set; }
            = AIQuotaStatus.Reserved;

        public string? ErrorMessage { get; set; }

        // Đồng ý xử lý ảnh cá nhân
        public bool ConsentGiven { get; set; }

        public DateTime? ConsentAt { get; set; }

        // Thời gian xử lý
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        public DateTime? ProcessingDeadline { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public int RetryCount { get; set; } = 0;

        // Chống tạo request trùng
        public Guid RequestKey { get; set; }

        // Navigation Properties
        public ApplicationUser? User { get; set; }

        public Shop? Shop { get; set; }

        public Product? Product { get; set; }

        public ProductImage? ProductImage { get; set; }

        public AIModel? AIModel { get; set; }

        public ShopAIUsage? ShopAIUsage { get; set; }
    }
}