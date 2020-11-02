using Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .IsRequired();

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}