using Domain.Entities.StockAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StockOnHoldConfiguration : IEntityTypeConfiguration<StockOnHold>
    {
        public void Configure(EntityTypeBuilder<StockOnHold> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .IsRequired();

            builder.Property(s => s.SessionId)
                .IsRequired();

            builder.Property(s => s.StockId)
                .IsRequired();

            builder.Property(s => s.ProductName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.ProductSize)
                .IsRequired(false)
                .HasMaxLength(10);

            builder.Property(s => s.ProductColor)
                .IsRequired(false)
                .HasMaxLength(10);
            
            builder.Property(s => s.PhotoUrl)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Quantity)
                .IsRequired();

            builder.HasCheckConstraint("CK_StockOnHold_Quantity", "[Quantity] >= 0");

            builder.Property(s => s.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasCheckConstraint("CK_StockOnHold_Price", "[Price] >= 0");

            builder.Property(s => s.ExpiryDate)
                .IsRequired();

            builder.HasIndex(s => s.StockId)
                .IsUnique(false);

            builder.HasOne(s => s.Stock)
                .WithOne(s => s.StockOnHold)
                .HasForeignKey<StockOnHold>(s => s.StockId);
        }
    }
}