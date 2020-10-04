namespace Domain.Entities.StockAggregate
{
    public class RelatedProduct
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int RelatedId { get; set; }
        public virtual Product Related { get; set; }
    }
}