using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Payment.Queries.GetPaymentMethods
{
    public class GetPaymentMethodsQueryHandler : IRequestHandler<GetPaymentMethodsQuery, List<PaymentDto>>
    {
        private readonly IAppDbContext _context;

        public GetPaymentMethodsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentDto>> Handle(GetPaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            return await _context.PaymentMethods
                .Select(PaymentDto.Projection)
                .ToListAsync(cancellationToken);
        }
    }
}