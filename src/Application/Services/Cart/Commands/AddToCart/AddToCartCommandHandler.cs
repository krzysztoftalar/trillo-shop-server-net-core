using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain.Entities.BuyerAggregate;
using Domain.Entities.StockAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Cart.Commands.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand>
    {
        private readonly IAppDbContext _context;
        private readonly ISessionService _sessionService;

        public AddToCartCommandHandler(IAppDbContext context, ISessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }

        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var stock = await _context.Stocks
                .Include(x => x.Product)
                .ThenInclude(x => x.Photos)
                .FirstOrDefaultAsync(x => x.Id == request.StockId, cancellationToken);

            if (request.Quantity > stock.Quantity)
            {
                throw new RestException(HttpStatusCode.BadRequest,
                    new { Stock = $"Not enough quantity in stock: {stock.Id}" });
            }

            stock.Quantity -= request.Quantity;

            var stocksOnHold = await _context.StocksOnHold
                .Where(x => x.SessionId == _sessionService.GetId())
                .ToListAsync(cancellationToken);

            if (stocksOnHold.Any(x => x.StockId == request.StockId))
            {
                stocksOnHold.Find(x => x.StockId == request.StockId)!.Quantity += request.Quantity;
            }
            else
            {
                await _context.StocksOnHold.AddAsync(new StockOnHold
                {
                    SessionId = _sessionService.GetId(),
                    StockId = request.StockId,
                    Quantity = request.Quantity,
                    ExpiryDate = DateTime.Now.AddMinutes(60)
                }, cancellationToken);
            }

            stocksOnHold.ForEach(x => x.ExpiryDate = DateTime.Now.AddMinutes(60));

            var cartProduct = new CartProduct
            {
                StockId = stock.Id,
                ProductName = stock.Product.Name,
                ProductSize = stock.ProductSize,
                ProductColor = stock.ProductColor,
                Price = stock.Price,
                Quantity = request.Quantity,
                PhotoUrl = stock.Product.Photos.First(x => x.IsMain).Url
            };

            _sessionService.AddToCart(cartProduct);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem adding to cart");
        }
    }
}