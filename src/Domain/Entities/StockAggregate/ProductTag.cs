namespace Domain.Entities.StockAggregate
{
    public class ProductTag
    {
        public string TagId { get; set; }
        public virtual Tag Tag { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}