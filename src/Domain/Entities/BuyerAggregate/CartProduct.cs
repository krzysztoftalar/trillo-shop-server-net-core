namespace Domain.Entities.BuyerAggregate
{
    public class CartProduct
    {
        public int StockId { get; set; }
        public string ProductName { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PhotoUrl { get; set; }
    }
}