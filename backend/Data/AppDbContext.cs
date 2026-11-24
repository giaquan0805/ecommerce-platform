using ecommerce_platform.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_platform.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Group 1: Users
        public DbSet<UserAddress> UserAddresses { get; set; }

        // Group 2: Shops
        public DbSet<Shop> Shops { get; set; }

        // Group 3: Products
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        // Group 4: Cart & Orders
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

        // Group 5: AI Virtual Try-On
        public DbSet<AIModel> AIModels { get; set; }
        public DbSet<AIPackage> AIPackages { get; set; }
        public DbSet<ShopAISubscription> ShopAISubscriptions { get; set; }
        public DbSet<ShopAIUsage> ShopAIUsages { get; set; }
        public DbSet<TryOnRequest> TryOnRequests { get; set; }

        // Group 6: Payments
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AIPackagePayment> AIPackagePayments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly
            );
        }
    }
}
