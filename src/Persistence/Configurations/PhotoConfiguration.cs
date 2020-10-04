using Domain.Entities.StockAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.ProductId)
                .IsRequired();

            builder.Property(p => p.Url)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.IsMain)
                .IsRequired();

            builder.HasOne(ph => ph.Product)
                .WithMany(p => p.Photos)
                .HasForeignKey(ph => ph.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}