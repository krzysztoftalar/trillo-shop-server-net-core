using Application.Dtos;
using MediatR;

namespace Application.Services.Order.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest
    {
        public int ShippingId { get; set; }
        public int PaymentId { get; set; }
        public string SessionId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}