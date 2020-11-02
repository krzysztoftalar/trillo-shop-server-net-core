using System.Reflection;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Domain.Entities.StockAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Address = Domain.Entities.StockAggregate.Address;

namespace Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressesTypes { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<RelatedProduct> RelatedProducts { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockCostHistory> StockCostHistories { get; set; }
        public DbSet<StockOnHold> StocksOnHold { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}