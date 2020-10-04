namespace Domain.Entities.StockAggregate
{
    public class AddressType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Address Address { get; set; }
    }
}