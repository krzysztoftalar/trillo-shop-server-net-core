using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Domain.Entities.StockAggregate;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<AppUser> Users { get; set; }
        
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<AddressType> AddressesTypes { get; set; }
        
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<RelatedProduct> RelatedProducts { get; set; }
        DbSet<ProductTag> ProductTags { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<Photo> Photos { get; set; }
        DbSet<Wishlist> Wishlists { get; set; }
        
        DbSet<Stock> Stocks { get; set; }
        DbSet<StockCostHistory> StockCostHistories { get; set; }
        DbSet<StockOnHold> StocksOnHold { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}