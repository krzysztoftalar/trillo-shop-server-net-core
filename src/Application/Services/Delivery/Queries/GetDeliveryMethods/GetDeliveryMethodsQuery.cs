using System.Collections.Generic;
using Application.Dtos;
using MediatR;

namespace Application.Services.Delivery.Queries.GetDeliveryMethods
{
    public class GetDeliveryMethodsQuery : IRequest<List<DeliveryDto>>
    {
    }
}