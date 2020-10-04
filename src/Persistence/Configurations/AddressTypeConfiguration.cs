using Domain.Entities.StockAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AddressTypeConfiguration : IEntityTypeConfiguration<AddressType>
    {
        public void Configure(EntityTypeBuilder<AddressType> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .IsRequired();

            builder.Property(a => a.Name)
                .HasMaxLength(20);

            builder.HasOne(at => at.Address)
                .WithOne(a => a.AddressType)
                .HasForeignKey<Address>(a => a.AddressTypeId);
        }
    }
}