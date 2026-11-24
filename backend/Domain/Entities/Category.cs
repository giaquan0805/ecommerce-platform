namespace ecommerce_platform.Domain.Entities
{
    public class Category
    {
        public long Id { get; set; }

        public long? ParentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public Category? Parent { get; set; }

        public ICollection<Category> Children { get; set; }
            = new List<Category>();

        public ICollection<Product> Products { get; set; }
            = new List<Product>();
    }
}