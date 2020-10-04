namespace Domain.Entities.StockAggregate
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Street { get; set; }
        public string HomeNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public int AddressTypeId { get; set; }
        public AddressType AddressType { get; set; }

        public string CustomerId { get; set; }
        public AppUser Customer { get; set; }
    }
}