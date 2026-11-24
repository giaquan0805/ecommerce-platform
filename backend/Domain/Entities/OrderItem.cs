namespace ecommerce_platform.Domain.Entities
{
    public class OrderItem
    {
        public long Id { get; set; }

        public long OrderId { get; set; }

        public long VariantId { get; set; }

        // Snapshot tại thời điểm đặt hàng
        public string ProductName { get; set; } = string.Empty;

        public string Sku { get; set; } = string.Empty;

        public string Size { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        // Navigation Properties
        public Order? Order { get; set; }

        public ProductVariant? Variant { get; set; }
    }
}