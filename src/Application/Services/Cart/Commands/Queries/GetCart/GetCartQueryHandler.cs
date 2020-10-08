using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using MediatR;

namespace Application.Services.Cart.Commands.Queries.GetCart
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartProductsEnvelope>
    {
        private readonly ISessionService _sessionService;

        public GetCartQueryHandler(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<CartProductsEnvelope> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cartProducts = await Task.Run(() => _sessionService.GetCart(CartProductDto.Projection).ToList(),
                cancellationToken);

            return new CartProductsEnvelope
            {
                CartProducts = cartProducts,
                TotalQty = cartProducts.Sum(x => x.Quantity),
                TotalValue = cartProducts.Sum(x => x.Price * x.Quantity)
            };
        }
    }
}