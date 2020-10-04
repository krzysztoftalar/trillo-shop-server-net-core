using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities.OrderAggregate
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderRef { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public OrderStatus Status { get; set; }
        public OrderAddress ShippingAddress { get; set; }

        public string CustomerId { get; set; }
        public virtual AppUser Customer { get; set; }

        public int DeliveryMethodId { get; set; }
        public virtual DeliveryMethod DeliveryMethod { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}