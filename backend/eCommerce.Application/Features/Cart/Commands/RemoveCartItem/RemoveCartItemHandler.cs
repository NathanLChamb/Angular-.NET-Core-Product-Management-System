using eCommerce.Application.Exceptions;
using eCommerce.Application.Features.Cart.DTOs;
using eCommerce.Application.Features.Cart.Mappings;
using eCommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Cart.Commands.RemoveCartItem
{
    public class RemoveCartItemHandler : IRequestHandler<RemoveCartItemCommand, ReadCartDto>
    {
        private readonly IeCommerceContext _context;

        public RemoveCartItemHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<ReadCartDto> Handle(
            RemoveCartItemCommand request,
            CancellationToken ct)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, ct);

            if (cart == null)
            {
                throw new NotFoundException("Cart not found.");
            }

            var item = cart.Items
                .FirstOrDefault(i =>
                    i.ProductVariantId == request.ProductVariantId);

            if (item == null)
            {
                throw new NotFoundException("Cart item not found.");
            }

            cart.Items.Remove(item);

            cart.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);

            return await _context.Carts
                .Where(c => c.Id == cart.Id)
                .ToCartDto()
                .FirstAsync(ct);
        }
    }
}
