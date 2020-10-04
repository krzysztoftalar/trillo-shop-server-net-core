using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Product.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsEnvelope>
    {
        private readonly IAppDbContext _context;

        public GetProductsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductsEnvelope> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.Products
                .Include(x => x.Photos)
                .Include(x => x.Stocks)
                .Include(x => x.Category)
                .Include(x => x.ProductTags)
                .Include(x => x.Reviews)
                .Include(x => x.OrderItems)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Predicate))
            {
                queryable = request.Predicate switch
                {
                    "men" => queryable.Where(x => x.ProductType == ProductType.Men),
                    "woman" => queryable.Where(x => x.ProductType == ProductType.Woman),
                    "featured" => queryable.Where(x => x.IsFeatured),
                    "promo" => queryable.Where(x => x.IsPromo),
                    _ => queryable
                };
            }

            if (!string.IsNullOrWhiteSpace(request.Tag))
            {
                var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == request.Tag, cancellationToken);

                if (tag != null)
                {
                    queryable = queryable.Where(x => x.ProductTags.Select(y => y.TagId).Contains(tag.Id));
                }
                else
                {
                    return new ProductsEnvelope();
                }
            }

            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                queryable = request.SortBy switch
                {
                    "popularity" => queryable.OrderByDescending(x => x.OrderItems.Sum(y => y.ProductId)),
                    "rating" => queryable.OrderByDescending(x =>
                        x.Reviews.Count == 0 ? 0 : x.Reviews.Sum(y => y.Rating) / (double) x.Reviews.Count),
                    "latest" => queryable.OrderByDescending(x => x.CreatedAt),
                    "priceDsc" => queryable.OrderByDescending(x => x.Stocks.Select(y => y.Price).FirstOrDefault()),
                    "priceAsc" => queryable.OrderBy(x => x.Stocks.Select(y => y.Price).FirstOrDefault()),
                    _ => queryable
                };
            }

            var products = queryable
                .Select(ProductDto.ProductsProjection);

            return new ProductsEnvelope
            {
                Products = await products.ToListAsync(cancellationToken: cancellationToken),
                ProductsCount = await products.CountAsync(cancellationToken: cancellationToken)
            };
        }
    }
}