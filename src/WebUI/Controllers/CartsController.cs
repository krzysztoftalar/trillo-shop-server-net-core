using System.Threading.Tasks;
using Application.Services.Cart.Commands.AddToCart;
using Application.Services.Cart.Commands.RemoveFromCart;
using Application.Services.Cart.Queries.GetCart;
using Domain.Entities.BuyerAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class CartsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<CartProductsEnvelope>> GetCart()
        {
            return await Mediator.Send(new GetCartQuery());
        }

        [HttpPost("{stockId}/{quantity}")]
        public async Task<ActionResult<Unit>> AddToCart(int stockId, int quantity)
        {
            return await Mediator.Send(new AddToCartCommand { StockId = stockId, Quantity = quantity });
        }

        [HttpPut("{stockId}/{quantity}")]
        public async Task<ActionResult<Unit>> RemoveFromCart(int stockId, int quantity)
        {
            return await Mediator.Send(new RemoveFromCartCommand { StockId = stockId, Quantity = quantity });
        }
    }
}