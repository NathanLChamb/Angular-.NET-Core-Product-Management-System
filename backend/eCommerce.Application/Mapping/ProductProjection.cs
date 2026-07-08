using eCommerce.Application.DTOs.Option;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Features.Categories.DTOs;
using eCommerce.Domain.Product;

namespace eCommerce.Application.Mapping
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
                    OptionValues = pv.ProductVariantOptionValues.Select(pvov => new ReadOptionValueDto
                    {
                        Id = pvov.OptionValue.Id,
                        Value = pvov.OptionValue.Value
                    }).ToList(),
                    CreatedAt = pv.CreatedAt,
                    UpdatedAt = pv.UpdatedAt
                }).ToList(),
            });
        }
    }
}
