using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Cart.Commands.RemoveFromCart
{
    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand>
    {
        private readonly IAppDbContext _context;
        private readonly ISessionService _sessionService;

        public RemoveFromCartCommandHandler(IAppDbContext context, ISessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }

        public async Task<Unit> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(x => x.Id == request.StockId, cancellationToken);

            var stockOnHold = await _context.StocksOnHold
                .FirstOrDefaultAsync(x => x.StockId == request.StockId && x.SessionId == _sessionService.GetId(),
                    cancellationToken);

            stock.Quantity += request.Quantity;
            stockOnHold.Quantity -= request.Quantity;

            if (stockOnHold.Quantity == 0)
            {
                _context.StocksOnHold.Remove(stockOnHold);
            }

            _sessionService.RemoveFromCart(request.StockId, request.Quantity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem removing from cart");
        }
    }
}