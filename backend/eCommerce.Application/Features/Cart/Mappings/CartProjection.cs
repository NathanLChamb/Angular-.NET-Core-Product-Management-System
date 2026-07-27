using eCommerce.Application.Features.Cart.DTOs;

namespace eCommerce.Application.Features.Cart.Mappings
{
    public static class CartProjection
    {
        public static IQueryable<ReadCartDto> ToCartDto(this IQueryable<Domain.Cart.Cart> query)
        {
            return query.Select(c => new ReadCartDto
            {
                Id = c.Id,
                Items = c.Items.Select(ci => new ReadCartItemDto
                {
                    Id = ci.Id,
                    ProductVariantId = ci.ProductVariantId,
                    ProductName = ci.ProductVariant.Product.Name,
                    OptionValues = ci.ProductVariant.ProductVariantOptionValues.Select(pvov => new ReadOptionValueFromCartDto
                    {
                        Value = pvov.OptionValue.Value
                    }).ToList(),
                    UnitPrice = ci.ProductVariant.Price,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.ProductVariant.Price * ci.Quantity
                }).ToList(),
                TotalPrice = c.Items.Sum(i => i.ProductVariant.Price * i.Quantity)
            });
        }
    }
}