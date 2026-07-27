using eCommerce.Application.Exceptions;
using eCommerce.Application.Features.Cart.DTOs;
using eCommerce.Application.Features.Cart.Mappings;
using eCommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Cart.Commands.AddCartItem
{
    public class AddCartItemHandler : IRequestHandler<AddCartItemCommand, ReadCartDto>
    {
        private readonly IeCommerceContext _context;
        public AddCartItemHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<ReadCartDto> Handle(AddCartItemCommand request, CancellationToken ct)
        {
            if (request.Quantity <= 0)
            {
                throw new BusinessRuleException("Quantity must be greater than zero.");
            }

            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, ct);

            if (cart == null)
            {
                cart = new Domain.Cart.Cart
                {
                    UserId = request.UserId
                };

                _context.Carts.Add(cart);
            }

            var productVariant = await _context.ProductVariants
                .Include(pv => pv.Product)
                .Include(pv => pv.ProductVariantOptionValues)
                    .ThenInclude(pvov => pvov.OptionValue)
                .FirstOrDefaultAsync(pv => pv.Id == request.ProductVariantId, ct);
            if (productVariant == null) throw new NotFoundException("Product variant not found.");

            var existingItem = cart.Items
                .FirstOrDefault(i => i.ProductVariantId == request.ProductVariantId);
            var newQuantity = existingItem == null ? request.Quantity: existingItem.Quantity + request.Quantity;
            if (newQuantity > productVariant.StockQuantity) throw new BusinessRuleException("Insufficient stock.");

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                cart.Items.Add(new Domain.Cart.CartItem
                {
                    ProductVariantId = productVariant.Id,
                    ProductVariant = productVariant,
                    Quantity = request.Quantity
                });
            }
            cart.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);

            return await _context.Carts
                .Where(c => c.Id == cart.Id)
                .ToCartDto()
                .FirstAsync(ct);
            //return new ReadCartDto
            //{
            //    Id = cart.Id,
            //    Items = cart.Items.Select(i => new ReadCartItemDto
            //    {
            //        Id = i.Id,
            //        ProductVariantId = i.ProductVariantId,
            //        ProductName = i.ProductVariant.Product.Name,
            //        OptionValues = i.ProductVariant.ProductVariantOptionValues.Select(pvov => new ReadOptionValueFromCartDto
            //        {
            //            Value = pvov.OptionValue.Value
            //        }).ToList(),
            //        UnitPrice = i.ProductVariant.Price,
            //        Quantity = i.Quantity,
            //        TotalPrice = i.ProductVariant.Price * i.Quantity
            //    }).ToList(),
            //    TotalPrice = cart.Items.Sum(i => i.ProductVariant.Price * i.Quantity)
            //};
        }
    }
}