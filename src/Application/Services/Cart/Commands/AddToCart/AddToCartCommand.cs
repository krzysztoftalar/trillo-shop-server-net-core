using MediatR;

namespace Application.Services.Cart.Commands.AddToCart
{
    public class AddToCartCommand : IRequest
    {
        public int StockId { get; set; }
        public int Quantity { get; set; }
    }
}