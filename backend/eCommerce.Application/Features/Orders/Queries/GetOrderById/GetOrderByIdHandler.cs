using eCommerce.Application.Features.Orders.DTOs;
using eCommerce.Application.Features.Orders.Mappings;
using eCommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdHandler
        : IRequestHandler<GetOrderByIdQuery, ReadOrderDto?>
    {
        private readonly IeCommerceContext _context;

        public GetOrderByIdHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<ReadOrderDto?> Handle(
            GetOrderByIdQuery request,
            CancellationToken ct)
        {
            return await _context.Orders
                .Where(o =>
                    o.Id == request.OrderId &&
                    o.UserId == request.UserId)
                .ToOrderDto()
                .FirstOrDefaultAsync(ct);
        }
    }
}
