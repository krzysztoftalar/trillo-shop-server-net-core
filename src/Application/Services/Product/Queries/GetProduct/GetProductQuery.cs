using Application.Dtos;
using MediatR;

namespace Application.Services.Product.Queries.GetProduct
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}