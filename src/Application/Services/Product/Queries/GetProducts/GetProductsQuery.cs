using MediatR;

namespace Application.Services.Product.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<ProductsEnvelope>
    {
        public string Predicate { get; set; }
        public string Tag { get; set; }
        public string SortBy { get; set; }
    }
}