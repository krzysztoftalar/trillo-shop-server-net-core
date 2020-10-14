using MediatR;

namespace Application.Services.Cart.Commands.RemoveFromCart
{
    public class RemoveFromCartCommand : IRequest
    {
        public int StockId { get; set; }
        public int Quantity { get; set; }
    }
}