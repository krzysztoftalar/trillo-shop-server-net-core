using Domain.Entities.StockAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class RelatedProductConfiguration : IEntityTypeConfiguration<RelatedProduct>
    {
        public void Configure(EntityTypeBuilder<RelatedProduct> builder)
        {
            builder.HasKey(p => new {p.ProductId, p.RelatedId});
            //
            // builder.HasOne(rp => rp.Product)
            //     .WithMany(p => p.RelatedProducts)
            //     .HasForeignKey(rp => rp.ProductId);
    
            builder.HasCheckConstraint("CK_RelatedProduct_RelatedId",
                "[ProductId] <> [RelatedId]");
        }
    }
}