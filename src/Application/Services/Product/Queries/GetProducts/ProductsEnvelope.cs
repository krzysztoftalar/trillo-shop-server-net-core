using System.Collections.Generic;
using Application.Dtos;

namespace Application.Services.Product.Queries.GetProducts
{
    public class ProductsEnvelope
    {
        public List<ProductDto> Products { get; set; }
        public int ProductsCount { get; set; }
    }
}