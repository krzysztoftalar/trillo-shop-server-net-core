using Domain.Entities.StockAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .IsRequired();
            
            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(30);
            
            builder.Property(a => a.CompanyName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.HomeNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(30);
            
            builder.Property(a => a.State)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasOne(a => a.Customer)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}