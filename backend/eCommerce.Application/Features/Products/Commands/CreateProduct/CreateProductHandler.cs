using eCommerce.Application.Features.Products.DTOs;
using eCommerce.Application.Features.Products.Mappings;
using eCommerce.Application.Interfaces;
using eCommerce.Domain.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ReadProductDto>
    {
        private readonly IeCommerceContext _context;
        public CreateProductHandler(IeCommerceContext context)
        {
            _context = context;
        }
        public async Task<ReadProductDto> Handle(CreateProductCommand request, CancellationToken ct)
        {
            var newProduct = new Product
            {
                Name = request.Name,
                Description = request.Description,
                ProductCategories = request.CategoryIds.Select(categoryId => new ProductCategory
                {
                    CategoryId = categoryId
                }).ToList(),
                ProductOptions = request.OptionIds.Select(optionId => new ProductOption
                {
                    OptionId = optionId
                }).ToList(),
                ProductVariants = request.ProductVariants.Select(pv => new ProductVariant
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

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == newProduct.Id)
                .ToProductDto()
                .FirstAsync(ct);
        }
    }
}
