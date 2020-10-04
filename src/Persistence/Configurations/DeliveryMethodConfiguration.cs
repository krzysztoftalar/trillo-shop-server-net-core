using Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .IsRequired();

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(d => d.DeliveryTime)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasCheckConstraint("CK_DeliveryMethod_Price", "[Price] >= 0");
        }
    }
}