using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Delivery.Queries.GetDeliveryMethods
{
    public class GetDeliveryMethodsQueryHandler : IRequestHandler<GetDeliveryMethodsQuery, List<DeliveryDto>>
    {
        private readonly IAppDbContext _context;

        public GetDeliveryMethodsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DeliveryDto>> Handle(GetDeliveryMethodsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.DeliveryMethods
                .Select(DeliveryDto.Projection)
                .ToListAsync(cancellationToken);
        }
    }
}