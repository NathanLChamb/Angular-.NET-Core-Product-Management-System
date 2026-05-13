using eCommercePractice4.Application.DTOs.Category;
using eCommercePractice4.Application.DTOs.Option;
using eCommercePractice4.Application.DTOs.Product;
using eCommercePractice4.Application.Exceptions;
using eCommercePractice4.Application.Interfaces;
using eCommercePractice4.Application.Shared;
using eCommercePractice4.Domain.Product;
using eCommercePractice4.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization.Metadata;

namespace eCommercePractice4.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly eCommercePracticeContext _context;
        public ProductService(eCommercePracticeContext context)
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

            var items = await query
                .Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize)
                .Select(p => new ReadProductDto
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
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();

            return new PagedResult<ReadProductDto>
            {
                Items = items,
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
                .Select(p => new ReadProductDto
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
                        Description= pc.Category.Description
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
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            if (product == null) throw new NotFoundException("Product not found from provided ID");

            return product;
        }

        public async Task<ReadProductDto> CreateProductAsync(CreateProductDto dto)
        {
            var newProduct = new Product
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
                    StockQuantity= pv.StockQuantity,
                    ProductVariantOptionValues = pv.OptionValueIds.Select(optionValueId => new ProductVariantOptionValue
                    {
                        OptionValueId = optionValueId
                    }).ToList()
                }).ToList()
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return await _context.Products
                .Where(p => p.Id == newProduct.Id)
                .Select(p => new ReadProductDto
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
                        }).ToList()
                    }).ToList()
                })
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

            if (product == null) throw new NotFoundException("product for update not found from provided ID");

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
                    .FirstOrDefault(pv => pv.Id == existingVariant.Id);

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

                    existingVariant.ProductVariantOptionValues.RemoveAll(existing =>
                        !dtoVariant.OptionValueIds.Contains(existing.OptionValueId));
                    existingVariant.ProductVariantOptionValues.AddRange(dtoVariant.OptionValueIds
                        .Where(optionValueId => !existingVariant.ProductVariantOptionValues
                        .Any(existing => existing.OptionValueId == optionValueId))
                        .Select(optionValueId => new ProductVariantOptionValue
                        {
                            OptionValueId = optionValueId
                        })
                    );
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
