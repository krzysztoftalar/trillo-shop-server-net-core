using MediatR;

namespace Application.Services.Cart.Queries.GetCart
{
    public class GetCartQuery : IRequest<CartProductsEnvelope>
    {
    }
}