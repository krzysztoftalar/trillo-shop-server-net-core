using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Product.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IAppDbContext _context;

        public GetProductQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var stocksOnHold = await _context.StocksOnHold
                .Where(x => x.ExpiryDate < DateTime.Now)
                .ToListAsync(cancellationToken);

            if (stocksOnHold.Count > 0)
            {
                var stocksToReturn = _context.Stocks
                    .AsEnumerable()
                    .Where(x => stocksOnHold.Any(y => y.StockId == x.Id));

                foreach (var stock in stocksToReturn)
                {
                    stock.Quantity += stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id)!.Quantity;
                }
                
                _context.StocksOnHold.RemoveRange(stocksOnHold);

                await _context.SaveChangesAsync(cancellationToken);
            }

            return await _context.Products
                .Include(x => x.Photos)
                .Include(x => x.Stocks)
                .Include(x => x.Category)
                .Include(x => x.ProductTags)
                .Include(x => x.Reviews)
                .ThenInclude(x => x.Author)
                .Include(x => x.OrderItems)
                .Where(x => x.Id == request.Id)
                .Select(ProductDto.ProductProjection)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}