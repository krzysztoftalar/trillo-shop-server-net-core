using System;
using Domain.Entities.StockAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .IsRequired(false)
                .HasMaxLength(3500);

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.Property(p => p.IsFeatured)
                .IsRequired()
                .HasDefaultValue(false);
            
            builder.Property(p => p.IsPromo)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(p => p.BgColor)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(p => p.ProductType)
                .IsRequired()
                .HasConversion(p => p.ToString(),
                    p => (ProductType) Enum.Parse(typeof(ProductType), p));

            builder.HasMany(p => p.RelatedProducts)
                .WithOne(rp => rp.Product)
                .HasForeignKey(rp => rp.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}