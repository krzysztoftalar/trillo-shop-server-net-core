using Application.Dtos;
using MediatR;

namespace Application.Services.Payment.Commands.CreateCheckoutSession
{
    public class CreateCheckoutSessionCommand : IRequest<PaymentSessionDto>
    {
        public int ShippingId { get; set; }
        public int PaymentId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}