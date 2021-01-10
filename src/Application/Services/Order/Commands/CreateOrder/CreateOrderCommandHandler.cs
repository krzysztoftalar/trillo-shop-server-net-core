using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using Domain.Entities.OrderAggregate;
using Domain.Entities.StockAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Address = Domain.Entities.OrderAggregate.Address;
using AppContext = Application.Infrastructure.AppContext;

namespace Application.Services.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IAppDbContext _context;
        private readonly ISessionService _sessionService;

        public CreateOrderCommandHandler(IAppDbContext context, ISessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var paymentMethod =
                await _context.PaymentMethods.FirstAsync(x => x.Id == request.PaymentId, cancellationToken);

            if (paymentMethod == null)
            {
                throw new RestException(HttpStatusCode.NotFound,
                    new { CreateOrder = $"Not found payment method: {request.PaymentId}" });
            }

            var deliveryMethod =
                await _context.DeliveryMethods.FirstAsync(x => x.Id == request.ShippingId, cancellationToken);

            if (deliveryMethod == null)
            {
                throw new RestException(HttpStatusCode.NotFound,
                    new { CreateOrder = $"Not found delivery method: {request.ShippingId}" });
            }

            var order = new Domain.Entities.OrderAggregate.Order
            {
                OrderRef = CreateOrderRef(),
                CustomerEmail = request.ShipToAddress.Email,
                DeliveryMethod = deliveryMethod,
                PaymentMethod = paymentMethod,
                ShippingAddress = MapAddress(request.ShipToAddress),
            };

            var stockOnHold = new List<StockOnHold>();

            switch (paymentMethod.Id)
            {
                case 1:
                case 2:
                {
                    var cartProducts = await Task.Run(() => _sessionService.GetCart(CartProductDto.Projection).ToList(),
                        cancellationToken);

                    order.Paid = false;
                    order.SessionId = _sessionService.GetId();
                    order.OrderItems = cartProducts.Select(x => new OrderItem
                    {
                        StockId = x.StockId,
                        ProductName = x.ProductName,
                        ProductSize = x.ProductSize,
                        ProductColor = x.ProductColor,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        ProductPhotoUrl = x.PhotoUrl,
                    }).ToList();

                    stockOnHold = await _context.StocksOnHold
                        .Where(x => x.SessionId == _sessionService.GetId())
                        .ToListAsync(cancellationToken);
                    break;
                }
                case 3:
                    stockOnHold = await _context.StocksOnHold
                        .Where(x => x.SessionId == request.SessionId)
                        .ToListAsync(cancellationToken);

                    order.Paid = true;
                    order.SessionId = request.SessionId;
                    order.OrderItems = stockOnHold.Select(x => new OrderItem
                    {
                        StockId = x.StockId,
                        ProductName = x.ProductName,
                        ProductSize = x.ProductSize,
                        ProductColor = x.ProductColor,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        ProductPhotoUrl = x.PhotoUrl,
                    }).ToList();
                    break;
            }

            await _context.Orders.AddAsync(order, cancellationToken);
            _context.StocksOnHold.RemoveRange(stockOnHold);
     
            AppContext.Current.Response.Cookies.Delete("Cart");

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem creating order");
        }

        public static Address MapAddress(AddressDto address)
        {
            return new Address
            {
                FirstName = address.FirstName,
                LastName = address.LastName,
                CompanyName = address.CompanyName,
                Email = address.Email,
                PhoneNumber = address.PhoneNumber,
                Country = address.Country,
                City = address.City,
                State = address.State,
                Street = address.Street,
                HomeNumber = address.HomeNumber,
                ZipCode = address.ZipCode,
            };
        }

        private string CreateOrderRef()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var result = new char[12];
            var rnd = new Random();

            do
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = chars[rnd.Next(chars.Length)];
                }
            } while (_context.Orders.Any(x => x.OrderRef == new string(result)));

            return new string(result);
        }
    }
}