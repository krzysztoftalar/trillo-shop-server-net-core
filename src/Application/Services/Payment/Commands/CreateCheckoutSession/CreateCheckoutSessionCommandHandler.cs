using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Infrastructure;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Payment.Commands.CreateCheckoutSession
{
    public class CreateCheckoutSessionCommandHandler : IRequestHandler<CreateCheckoutSessionCommand, PaymentSessionDto>
    {
        private readonly IPaymentService _paymentService;
        private readonly ISessionService _sessionService;
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _http;

        public CreateCheckoutSessionCommandHandler(IPaymentService paymentService, ISessionService sessionService,
            IAppDbContext context, IHttpContextAccessor http)
        {
            _paymentService = paymentService;
            _sessionService = sessionService;
            _context = context;
            _http = http;
        }

        public async Task<PaymentSessionDto> Handle(CreateCheckoutSessionCommand request,
            CancellationToken cancellationToken)
        {
            var cart = await Task.Run(() => _sessionService.GetCart(CartProductDto.Projection).ToList(),
                cancellationToken);

            var deliveryMethod =
                await _context.DeliveryMethods.FirstAsync(x => x.Id == request.ShippingId, cancellationToken);

            if (deliveryMethod == null)
            {
                throw new RestException(HttpStatusCode.NotFound,
                    new { CreateCheckoutSession = $"Not found delivery method: {request.ShippingId}" });
            }

            var sessionId = await _paymentService.CreateCheckoutSession(cart, deliveryMethod, request.ShipToAddress, _sessionService.GetId());

            AppContext.Current.Response.Cookies.Delete("Cart");
            
            return new PaymentSessionDto { SessionId = sessionId };
        }
    }
}