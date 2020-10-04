namespace Domain.Entities.StockAggregate
{
    public class Wishlist
    {
        public string CustomerId { get; set; }
        public virtual AppUser Customer { get; set; }

        public int StockId { get; set; }
        public virtual Stock Stock { get; set; }
    }
}