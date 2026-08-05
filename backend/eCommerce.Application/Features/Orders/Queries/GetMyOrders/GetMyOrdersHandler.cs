using eCommerce.Application.Features.Orders.DTOs;
using eCommerce.Application.Features.Orders.Mappings;
using eCommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Orders.Queries.GetMyOrders
{
    public class GetMyOrdersHandler
        : IRequestHandler<GetMyOrdersQuery, List<ReadOrderDto>>
    {
        private readonly IeCommerceContext _context;

        public GetMyOrdersHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<List<ReadOrderDto>> Handle(
            GetMyOrdersQuery request,
            CancellationToken ct)
        {
            return await _context.Orders
                .Where(o => o.UserId == request.UserId)
                .OrderByDescending(o => o.OrderDate)
                .ToOrderDto()
                .ToListAsync(ct);
        }
    }
}
