namespace Domain.Entities.StockAggregate
{
    public class Photo
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}