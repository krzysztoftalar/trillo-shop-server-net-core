using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities.OrderAggregate
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderRef { get; set; }
        public string SessionId { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; }

        public string CustomerId { get; set; }
        public int DeliveryMethodId { get; set; }
        public int PaymentMethodId { get; set; }
        public bool Paid { get; set; }
        
        public virtual AppUser Customer { get; set; }
        public virtual DeliveryMethod DeliveryMethod { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}