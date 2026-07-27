using eCommerce.Application.Features.Cart.DTOs;
using eCommerce.Application.Features.Cart.Mappings;
using eCommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Cart.Queries.GetCart
{
    public class GetCartHandler : IRequestHandler<GetCartQuery, ReadCartDto>
    {
        private readonly IeCommerceContext _context;

        public GetCartHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<ReadCartDto> Handle(GetCartQuery request, CancellationToken ct)
        {
            var cart = await _context.Carts
                .Where(c => c.UserId == request.UserId)
                .ToCartDto()
                .FirstOrDefaultAsync(ct);

            if (cart != null)
            {
                return cart;
            }

            return new ReadCartDto
            {
                Items = new(),
                TotalPrice = 0
            };
        }
    }
}
