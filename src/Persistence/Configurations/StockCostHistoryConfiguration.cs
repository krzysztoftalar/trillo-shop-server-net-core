using Domain.Entities.StockAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StockCostHistoryConfiguration : IEntityTypeConfiguration<StockCostHistory>
    {
        public void Configure(EntityTypeBuilder<StockCostHistory> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();
            
            builder.Property(s => s.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasCheckConstraint("CK_StockCostHistory_Price", "[Price] >= 0");

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("getdate()");
            
            builder.HasOne(sh => sh.Stock)
                .WithMany(s => s.StockCostHistories)
                .HasForeignKey(sh => sh.StockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}