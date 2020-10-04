using Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.OrderId)
                .IsRequired();

            builder.Property(o => o.ProductId)
                .IsRequired();

            builder.Property(o => o.ProductName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.ProductPhotoUrl)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.ProductSize)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(o => o.ProductColor)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(o => o.Quantity)
                .IsRequired();

            builder.HasCheckConstraint("CK_OrderItem_Quantity", "[Quantity] >= 0");

            builder.Property(o => o.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasCheckConstraint("CK_OrderItem_Price", "[Price] >= 0");

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}