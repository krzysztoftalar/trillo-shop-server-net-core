using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Entities.OrderAggregate;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreateCheckoutSession(IEnumerable<CartProductDto> cart, DeliveryMethod deliveryMethod,
            AddressDto address, string sessionId);
    }
}