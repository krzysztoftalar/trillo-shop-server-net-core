using Domain.Entities.StockAggregate;

namespace Domain.Entities.OrderAggregate
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int StockId { get; set; }

        public string ProductName { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ProductPhotoUrl { get; set; }

        public virtual Order Order { get; set; }
        public virtual Stock Stock { get; set; }
    }
}