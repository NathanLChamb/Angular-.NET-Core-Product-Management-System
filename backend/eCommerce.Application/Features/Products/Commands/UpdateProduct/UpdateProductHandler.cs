using eCommerce.Application.Exceptions;
using eCommerce.Application.Interfaces;
using eCommerce.Domain.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IeCommerceContext _context;
        public UpdateProductHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken ct)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.ProductOptions)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantOptionValues)
                .FirstOrDefaultAsync(p => p.Id == request.Id, ct);
            if (product == null) throw new NotFoundException("Product for update not found from provided ID");

            product.Name = request.Name ?? product.Name;
            product.Description = request.Description ?? product.Description;
            product.UpdatedAt = DateTime.UtcNow;

            product.ProductCategories.RemoveAll(existing => !request.CategoryIds.Contains(existing.CategoryId));
            product.ProductCategories.AddRange(request.CategoryIds
                .Where(categoryId => !product.ProductCategories
                .Any(existing => existing.CategoryId == categoryId))
                .Select(categoryId => new ProductCategory
                {
                    CategoryId = categoryId
                })
            );

            product.ProductOptions.RemoveAll(existing => !request.OptionIds.Contains(existing.OptionId));
            product.ProductOptions.AddRange(request.OptionIds
                .Where(optionId => !product.ProductOptions
                .Any(existing => existing.OptionId == optionId))
                .Select(optionId => new ProductOption
                {
                    OptionId = optionId
                })
            );

            product.ProductVariants.RemoveAll(existing => !request.ProductVariants.Any(_request => _request.Id == existing.Id));
            product.ProductVariants.AddRange(request.ProductVariants
                .Where(_request => !_request.Id.HasValue)
                .Select(_request => new ProductVariant
                {
                    Sku = _request.Sku,
                    Price = _request.Price,
                    StockQuantity = _request.StockQuantity,
                    ProductVariantOptionValues = _request.OptionValueIds.Select(optionValueId => new ProductVariantOptionValue
                    {
                        OptionValueId = optionValueId
                    }).ToList()
                })
            );
            foreach (var existingVariant in product.ProductVariants)
            {
                var requestVariant = request.ProductVariants
                    .FirstOrDefault(_request => _request.Id == existingVariant.Id);

                if (requestVariant != null)
                {
                    if (requestVariant.Sku != existingVariant.Sku)
                    {
                        existingVariant.Sku = requestVariant.Sku;
                    }
                    if (requestVariant.Price != existingVariant.Price)
                    {
                        existingVariant.Price = requestVariant.Price;
                    }
                    if (requestVariant.StockQuantity != existingVariant.StockQuantity)
                    {
                        existingVariant.StockQuantity = requestVariant.StockQuantity;
                    }

                    existingVariant.ProductVariantOptionValues.RemoveAll(existing => !requestVariant.OptionValueIds.Contains(existing.OptionValueId));
                    existingVariant.ProductVariantOptionValues.AddRange(requestVariant.OptionValueIds
                        .Where(optionValueId => !existingVariant.ProductVariantOptionValues
                        .Any(existing => existing.OptionValueId == optionValueId))
                        .Select(optionValueId => new ProductVariantOptionValue
                        {
                            OptionValueId = optionValueId
                        })
                    );
                }
            }

            await _context.SaveChangesAsync(ct);
        }
    }
}
