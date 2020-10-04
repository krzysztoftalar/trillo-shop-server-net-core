using System;

namespace Domain.Entities.StockAggregate
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public string AuthorId { get; set; }
        public virtual AppUser Author { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}