using Domain.Entities.StockAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .IsRequired();

            builder.Property(s => s.Quantity)
                .IsRequired();

            builder.HasCheckConstraint("CK_Stock_Quantity", "[Quantity] >= 0");

            builder.Property(s => s.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasCheckConstraint("CK_Stock_Price", "[Price] >= 0");

            builder.Property(s => s.ProductSize)
                .IsRequired(false)
                .HasMaxLength(10);

            builder.Property(s => s.ProductColor)
                .IsRequired(false)
                .HasMaxLength(10);

            builder.HasOne(s => s.Product)
                .WithMany(p => p.Stocks)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}