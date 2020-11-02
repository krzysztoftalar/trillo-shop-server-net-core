using System.Collections.Generic;
using Application.Dtos;
using MediatR;

namespace Application.Services.Payment.Queries.GetPaymentMethods
{
    public class GetPaymentMethodsQuery : IRequest<List<PaymentDto>>
    {
    }
}