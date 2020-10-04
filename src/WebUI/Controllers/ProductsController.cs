using System.Threading.Tasks;
using Application.Services.Product.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ProductsEnvelope>> GetProducts([FromQuery] GetProductsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}