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

            builder.Property(s => s.Quantity)
                .IsRequired();
            
            builder.HasCheckConstraint("CK_StockOnHold_Quantity", "[Quantity] >= 0");

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