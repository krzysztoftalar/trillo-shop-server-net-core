using Domain.Entities.StockAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(w => new {w.CustomerId, w.StockId});

            builder.HasOne(w => w.Customer)
                .WithMany(u => u.Wishlists)
                .HasForeignKey(w => w.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(w => w.Stock)
                .WithMany(s => s.Wishlists)
                .HasForeignKey(w => w.StockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}