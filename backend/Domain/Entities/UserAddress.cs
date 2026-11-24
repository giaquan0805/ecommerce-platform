namespace ecommerce_platform.Domain.Entities
{
    public class UserAddress
    {
        public long Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string ReceiverName { get; set; } = string.Empty;

        public string ReceiverPhone { get; set; } = string.Empty;

        public string Province { get; set; } = string.Empty;

        public string Ward { get; set; } = string.Empty;

        public string AddressLine { get; set; } = string.Empty;

        public bool IsDefault { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public ApplicationUser? User { get; set; }
    }
}