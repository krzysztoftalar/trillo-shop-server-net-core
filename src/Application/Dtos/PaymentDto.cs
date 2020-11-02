using System;
using System.Linq.Expressions;
using Domain.Entities.OrderAggregate;

namespace Application.Dtos
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static readonly Expression<Func<PaymentMethod, PaymentDto>> Projection =
            payment => new PaymentDto
            {
                Id = payment.Id,
                Name = payment.Name,
                Description = payment.Description,
            };
    }
}