using Domain.Entities.StockAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .IsRequired();

            builder.Property(r => r.Comment)
                .IsRequired()
                .HasMaxLength(3000);

            builder.Property(r => r.Rating)
                .IsRequired();

            builder.HasCheckConstraint("CK_Review_Rating", "[Rating] BETWEEN 1 And 5");

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.Property(r => r.AuthorId)
                .IsRequired();

            builder.Property(r => r.ProductId)
                .IsRequired();

            builder.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}