namespace Domain.Entities.OrderAggregate
{
    public class DeliveryMethod 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}