using System.Threading.Tasks;
using Application.Services.Order.Commands.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class OrdersController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            // HttpContext.Response.Cookies.Delete("Cart");
            return await Mediator.Send(command);
        }
    }
}