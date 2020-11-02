using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities.OrderAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Infrastructure.Payment
{
    public class StripePaymentService : IPaymentService
    {
        private readonly IOptions<StripeOptions> _options;

        public StripePaymentService(IOptions<StripeOptions> options)
        {
            _options = options;
            StripeConfiguration.ApiKey = _options.Value.SecretKey;
        }

        public async Task<string> CreateCheckoutSession(IEnumerable<CartProductDto> cart, DeliveryMethod deliveryMethod,
            AddressDto address, string sessionId)
        {
            var lineItems = cart.Select(item =>
                new SessionLineItemOptions
                {
                    Quantity = item.Quantity,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = item.Price * 100,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName,
                            Images = new List<string> { item.PhotoUrl },
                            Metadata = new Dictionary<string, string>
                            {
                                { "StockId", item.StockId.ToString() },
                                { "ProductColor", item.ProductColor },
                                { "ProductSize", item.ProductSize },
                            }
                        }
                    },
                }).ToList();

            lineItems.Add(new SessionLineItemOptions
            {
                Quantity = 1,
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = deliveryMethod.Price * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Shipping",
                    }
                },
            });

            var options = new SessionCreateOptions
            {
                Mode = "payment",
                SuccessUrl = $"{_options.Value.Domain}/stripe-payment/success",
                CancelUrl = $"{_options.Value.Domain}/stripe-payment/canceled",
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                CustomerEmail = address.Email,
                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    ReceiptEmail = address.Email,
                    Metadata = new Dictionary<string, string>
                    {
                        { "ShippingId", deliveryMethod.Id.ToString() },
                        { "SessionId", sessionId }
                    },
                    Shipping = new ChargeShippingOptions
                    {
                        Name = string.Join("/", address.FirstName, address.LastName, address.CompanyName),
                        Phone = address.PhoneNumber,
                        Address = new AddressOptions
                        {
                            Country = address.Country,
                            City = address.City,
                            State = address.State,
                            PostalCode = address.ZipCode,
                            Line1 = address.Street,
                            Line2 = address.HomeNumber
                        }
                    },
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Id;
        }
    }
}