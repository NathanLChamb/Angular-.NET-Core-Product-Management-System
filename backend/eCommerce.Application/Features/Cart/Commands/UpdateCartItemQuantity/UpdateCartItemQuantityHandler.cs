using eCommerce.Application.Exceptions;
using eCommerce.Application.Features.Cart.DTOs;
using eCommerce.Application.Features.Cart.Mappings;
using eCommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity
{
    public class UpdateCartItemQuantityHandler : IRequestHandler<UpdateCartItemQuantityCommand, ReadCartDto>
    {
        private readonly IeCommerceContext _context;

        public UpdateCartItemQuantityHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<ReadCartDto> Handle(
            UpdateCartItemQuantityCommand request,
            CancellationToken ct)
        {
            if (request.Quantity <= 0)
            {
                throw new BusinessRuleException(
                    "Quantity must be greater than zero.");
            }

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

            var productVariant = await _context.ProductVariants
                .FirstOrDefaultAsync(
                    pv => pv.Id == request.ProductVariantId,
                    ct);

            if (productVariant == null)
            {
                throw new NotFoundException("Product variant not found.");
            }

            if (request.Quantity > productVariant.StockQuantity)
            {
                throw new BusinessRuleException(
                    "Insufficient stock.");
            }

            item.Quantity = request.Quantity;

            cart.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);

            return await _context.Carts
                .Where(c => c.Id == cart.Id)
                .ToCartDto()
                .FirstAsync(ct);
        }
    }
}
