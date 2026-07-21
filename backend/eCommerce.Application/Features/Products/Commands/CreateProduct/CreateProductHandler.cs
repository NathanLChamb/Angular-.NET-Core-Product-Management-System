using eCommerce.Application.Features.Products.DTOs;
using eCommerce.Application.Features.Products.Interfaces;
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
        private readonly IProductValidationService _validationService;
        public CreateProductHandler(IeCommerceContext context, IProductValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
        }
        public async Task<ReadProductDto> Handle(CreateProductCommand request, CancellationToken ct)
        {
            await _validationService.ValidateCategoriesAsync(request.CategoryIds, ct);
            await _validationService.ValidateOptionsAsync(request.OptionIds, ct);
            var optionValueIds = request.ProductVariants
                .SelectMany(v => v.OptionValueIds)
                .ToList();
            await _validationService.ValidateOptionValuesAsync(optionValueIds, ct);
            await _validationService.ValidateOptionValuesBelongToOptionsAsync(request.OptionIds, optionValueIds, ct);

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
