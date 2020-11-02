using System;

namespace Domain.Entities.StockAggregate
{
    public class StockOnHold
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string ProductName { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime ExpiryDate { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }
    }
}