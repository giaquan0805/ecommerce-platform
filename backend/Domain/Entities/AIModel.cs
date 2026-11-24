namespace ecommerce_platform.Domain.Entities
{
    public class AIModel
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Provider { get; set; } = string.Empty;

        public string ModelCode { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public ICollection<TryOnRequest> TryOnRequests { get; set; }
            = new List<TryOnRequest>();
    }
}