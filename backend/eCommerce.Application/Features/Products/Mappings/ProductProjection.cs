using eCommerce.Application.Features.Categories.DTOs;
using eCommerce.Application.Features.Options.DTOs;
using eCommerce.Application.Features.Products.DTOs;
using eCommerce.Application.Features.Products.Images.DTOs;
using eCommerce.Domain.Product;

namespace eCommerce.Application.Features.Products.Mappings
{
    public static class ProductProjection
    {
        public static IQueryable<ReadProductDto> ToProductDto(this IQueryable<Product> query)
        {
            return query.Select(p => new ReadProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                Categories = p.ProductCategories.Select(pc => new ReadCategoryDto
                {
                    Id = pc.Category.Id,
                    Name = pc.Category.Name,
                    Description = pc.Category.Description
                }).ToList(),
                Options = p.ProductOptions.Select(po => new ReadOptionFromProductDto
                {
                    Id = po.Option.Id,
                    Name = po.Option.Name
                }).ToList(),
                ProductVariants = p.ProductVariants.Select(pv => new ReadProductVariantDto
                {
                    Id = pv.Id,
                    Sku = pv.Sku,
                    Price = pv.Price,
                    StockQuantity = pv.StockQuantity,
                    OptionValues = pv.ProductVariantOptionValues.Select(pvov => new ReadOptionValueFromProductVariantDto
                    {
                        Id = pvov.OptionValue.Id,
                        Value = pvov.OptionValue.Value,
                        OptionId = pvov.OptionValue.OptionId,
                        OptionName = pvov.OptionValue.Option.Name
                    }).ToList(),
                    CreatedAt = pv.CreatedAt,
                    UpdatedAt = pv.UpdatedAt
                }).ToList(),
                ProductImages = p.ProductImages.Select(pi => new ProductImageDto
                {
                    Id = pi.Id,
                    Url = pi.Url,
                    DisplayOrder = pi.DisplayOrder
                }).ToList(),
                ProductOptionValueImages = p.ProductOptionValueImages.Select(image => new ProductOptionValueImageDto
                {
                    Id = image.Id,
                    Url = image.Url,
                    DisplayOrder = image.DisplayOrder,
                    IsDefault = image.IsDefault,
                    OptionValueIds = image.OptionValues.Select(x => x.OptionValueId).ToList()
                })
                .ToList(),
            });
        }
    }
}
