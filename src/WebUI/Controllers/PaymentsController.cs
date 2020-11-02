using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Application.Services.Order.Commands.CreateOrder;
using Application.Services.Payment.Commands.CreateCheckoutSession;
using Application.Services.Payment.Queries.GetPaymentMethods;
using Infrastructure.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace WebUI.Controllers
{
    public class PaymentsController : BaseController
    {
        private readonly IOptions<StripeOptions> _options;

        public PaymentsController(IOptions<StripeOptions> options)
        {
            _options = options;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaymentDto>>> GetPaymentMethods()
        {
            return await Mediator.Send(new GetPaymentMethodsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<PaymentSessionDto>> CreateStripeCheckoutSession(
            [FromBody] CreateCheckoutSessionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebHook()
        {
            using var sr = new StreamReader(HttpContext.Request.Body);
            var json = await sr.ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
                    _options.Value.WebHook, 1000, false);

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var service = new SessionService();
                    var paymentIntentService = new PaymentIntentService();

                    var products = await service.ListLineItemsAsync(session?.Id);
                    var payment = await paymentIntentService.GetAsync(session?.PaymentIntentId);

                    await Mediator.Send(new CreateOrderCommand
                    {
                        ShipToAddress = new AddressDto
                        {
                            FirstName = payment.Shipping.Name.Split('/')[0],
                            LastName = payment.Shipping.Name.Split('/')[1],
                            CompanyName = payment.Shipping.Name.Split('/')[2],
                            Email = payment.ReceiptEmail,
                            PhoneNumber = payment.Shipping.Phone,
                            Country = payment.Shipping.Address.Country,
                            City = payment.Shipping.Address.City,
                            State = payment.Shipping.Address.State,
                            ZipCode = payment.Shipping.Address.PostalCode,
                            Street = payment.Shipping.Address.Line1,
                            HomeNumber = payment.Shipping.Address.Line2,
                        },
                        ShippingId = int.Parse(payment.Metadata.First(x => x.Key == "ShippingId").Value),
                        SessionId = payment.Metadata.First(x => x.Key == "SessionId").Value,
                        PaymentId = 3
                    });
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}