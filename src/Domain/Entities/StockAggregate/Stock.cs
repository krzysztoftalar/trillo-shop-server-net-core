using System.Collections.Generic;
using Domain.Entities.OrderAggregate;

namespace Domain.Entities.StockAggregate
{
    public class Stock
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public virtual StockOnHold StockOnHold { get; set; }
        public virtual ICollection<StockCostHistory> StockCostHistories { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}