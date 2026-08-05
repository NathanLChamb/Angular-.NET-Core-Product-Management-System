using eCommerce.Application.Features.Orders.DTOs;
using eCommerce.Application.Features.Orders.Mappings;
using eCommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<ReadOrderFromAdminDto>>
    {
        private readonly IeCommerceContext _context;
        public GetAllOrdersHandler(IeCommerceContext context)
        {
            _context = context;
        }
        public async Task<List<ReadOrderFromAdminDto>> Handle(GetAllOrdersQuery request, CancellationToken ct)
        {
            return await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .ToAdminOrderDto()
                .ToListAsync(ct);
        }
    }
}
