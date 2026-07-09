using eCommerce.Application.Exceptions;
using eCommerce.Application.Features.Products.DTOs;
using eCommerce.Application.Features.Products.Mappings;
using eCommerce.Application.Interfaces;
using eCommerce.Application.Shared;
using eCommerce.Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IeCommerceContext _context;
        public ProductService(IeCommerceContext context)
        {  
            _context = context;
        }

        public async Task<PagedResult<ReadProductDto>> GetAllProductsAsync(PaginationParams pageParams)
        {
            var query = _context.Products
                .AsNoTracking()
                .OrderBy(p => p.Id);

            var totalCount = await query
                .CountAsync();

            var products = await query
                .Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize)
                .ToProductDto()
                .ToListAsync();

            return new PagedResult<ReadProductDto>
            {
                Items = products,
                TotalCount = totalCount,
                PageNumber = pageParams.PageNumber,
                PageSize = pageParams.PageSize
            };
        }
        public async Task<ReadProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .ToProductDto()
                .FirstOrDefaultAsync();
            if (product == null) throw new NotFoundException("Product not found from provided ID");

            return product;
        }
        public async Task<ReadProductDto> CreateProductAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                ProductCategories = dto.CategoryIds.Select(categoryId => new ProductCategory
                {
                    CategoryId = categoryId
                }).ToList(),
                ProductOptions = dto.OptionIds.Select(optionId => new ProductOption
                {
                    OptionId = optionId
                }).ToList(),
                ProductVariants = dto.ProductVariants.Select(pv => new ProductVariant
                {
                    Sku = pv.Sku,
                    Price = pv.Price,
                    StockQuantity = pv.StockQuantity,
                    ProductVariantOptionValues = pv.OptionValueIds.Select(optionValueId => new ProductVariantOptionValue
                    {
                        OptionValueId = optionValueId
                    }).ToList()
                }).ToList()
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == product.Id)
                .ToProductDto()
                .FirstAsync();
        }
        public async Task UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.ProductOptions)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantOptionValues)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) throw new NotFoundException("Product for update not found from provided ID");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.UpdatedAt = DateTime.UtcNow;

            product.ProductCategories.RemoveAll(existing => !dto.CategoryIds.Contains(existing.CategoryId));
            product.ProductCategories.AddRange(dto.CategoryIds
                .Where(categoryId => !product.ProductCategories
                    .Any(existing => existing.CategoryId == categoryId))
                .Select(categoryId => new ProductCategory
                {
                    CategoryId = categoryId
                })
            );

            product.ProductOptions.RemoveAll(existing => !dto.OptionIds.Contains(existing.OptionId));
            product.ProductOptions.AddRange(dto.OptionIds
                .Where(optionId => !product.ProductOptions
                    .Any(existing => existing.OptionId == optionId))
                .Select(optionId => new ProductOption
                {
                    OptionId = optionId
                })
            );

            product.ProductVariants.RemoveAll(existing => !dto.ProductVariants.Any(_dto => _dto.Id == existing.Id));
            product.ProductVariants.AddRange(dto.ProductVariants
                .Where(_dto => !_dto.Id.HasValue)
                .Select(_dto => new ProductVariant
                {
                    Sku = _dto.Sku,
                    Price = _dto.Price,
                    StockQuantity = _dto.StockQuantity,
                    ProductVariantOptionValues = _dto.OptionValueIds.Select(optionValueId => new ProductVariantOptionValue
                    {
                        OptionValueId = optionValueId
                    }).ToList()
                })
            );
            foreach (var existingVariant in product.ProductVariants)
            {
                var dtoVariant = dto.ProductVariants
                    .FirstOrDefault(_dto => _dto.Id == existingVariant.Id);

                if (dtoVariant != null)
                {
                    if (dtoVariant.Sku != existingVariant.Sku)
                    {
                        existingVariant.Sku = dtoVariant.Sku;
                    }

                    if (dtoVariant.Price != existingVariant.Price)
                    {
                        existingVariant.Price = dtoVariant.Price;
                    }

                    if (dtoVariant.StockQuantity != existingVariant.StockQuantity)
                    {
                        existingVariant.StockQuantity = dtoVariant.StockQuantity;
                    }

                    existingVariant.ProductVariantOptionValues.RemoveAll(existing => !dtoVariant.OptionValueIds.Contains(existing.OptionValueId));
                    existingVariant.ProductVariantOptionValues.AddRange(dtoVariant.OptionValueIds
                        .Where(optionValueId => !existingVariant.ProductVariantOptionValues
                            .Any(existing => existing.OptionValueId == optionValueId))
                        .Select(optionValueId => new ProductVariantOptionValue
                        {
                            OptionValueId = optionValueId
                        })
                    );
                    existingVariant.UpdatedAt = DateTime.UtcNow;
                }
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new NotFoundException("Product for deletion not found from provided ID");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
