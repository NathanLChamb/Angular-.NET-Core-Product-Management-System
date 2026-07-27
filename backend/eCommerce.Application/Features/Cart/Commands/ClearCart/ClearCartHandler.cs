using eCommerce.Application.Exceptions;
using eCommerce.Application.Features.Cart.DTOs;
using eCommerce.Application.Features.Cart.Mappings;
using eCommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Cart.Commands.ClearCart
{
    public class ClearCartHandler : IRequestHandler<ClearCartCommand, ReadCartDto>
    {
        private readonly IeCommerceContext _context;

        public ClearCartHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<ReadCartDto> Handle(
            ClearCartCommand request,
            CancellationToken ct)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, ct);

            if (cart == null)
            {
                throw new NotFoundException("Cart not found.");
            }

            cart.Items.Clear();
            cart.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);

            return await _context.Carts
                .Where(c => c.Id == cart.Id)
                .ToCartDto()
                .FirstAsync(ct);
        }
    }
}
