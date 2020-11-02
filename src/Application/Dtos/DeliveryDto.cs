using System;
using System.Linq.Expressions;
using Domain.Entities.OrderAggregate;

namespace Application.Dtos
{
    public class DeliveryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public static readonly Expression<Func<DeliveryMethod, DeliveryDto>> Projection =
            delivery => new DeliveryDto
            {
                Id = delivery.Id,
                Name = delivery.Name,
                DeliveryTime = delivery.DeliveryTime,
                Description = delivery.Description,
                Price = delivery.Price,
            };
    }
}