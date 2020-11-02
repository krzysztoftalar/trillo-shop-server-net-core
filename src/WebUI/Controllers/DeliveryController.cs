using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Services.Delivery.Queries.GetDeliveryMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class DeliveryController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<DeliveryDto>>> GetDeliveryMethods()
        {
            return await Mediator.Send(new GetDeliveryMethodsQuery());
        }
    }
}