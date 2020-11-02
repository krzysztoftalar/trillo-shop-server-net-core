using System;
using Domain.Entities.OrderAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .IsRequired();

            builder.Property(o => o.OrderRef)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(o => o.SessionId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.CustomerEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.OrderDate)
                .IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.Property(o => o.ShipDate)
                .IsRequired(false);

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion(o => o.ToString(),
                    o => (OrderStatus) Enum.Parse(typeof(OrderStatus), o))
                .HasDefaultValue(OrderStatus.Pending);

            builder.OwnsOne(o => o.ShippingAddress, a => { a.WithOwner(); });

            builder.HasIndex(o => o.CustomerId)
                .IsUnique(false);

            builder.Property(o => o.CustomerId)
                .IsRequired(false);

            builder.HasOne(o => o.Customer)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.DeliveryMethodId)
                .IsRequired();
            
            builder.Property(o => o.PaymentMethodId)
                .IsRequired();
            
            builder.Property(o => o.Paid)
                .IsRequired();
        }
    }
}