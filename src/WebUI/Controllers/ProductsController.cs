using System.Threading.Tasks;
using Application.Dtos;
using Application.Services.Product.Queries.GetProduct;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            return await Mediator.Send(new GetProductQuery { Id = id });
        }
    }
}