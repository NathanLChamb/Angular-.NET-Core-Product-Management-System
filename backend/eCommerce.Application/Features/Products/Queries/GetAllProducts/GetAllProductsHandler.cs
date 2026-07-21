using eCommerce.Application.Features.Products.DTOs;
using eCommerce.Application.Features.Products.Filters;
using eCommerce.Application.Features.Products.Mappings;
using eCommerce.Application.Interfaces;
using eCommerce.Application.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, PagedResult<ReadProductDto>>
    {
        private readonly IeCommerceContext _context;
        public GetAllProductsHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ReadProductDto>> Handle(GetAllProductsQuery request, CancellationToken ct)
        {
            var productsQuery = _context.Products
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Filter.Search))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.ToLower().Contains(request.Filter.Search.ToLower()));
            }

            if (request.Filter.CategoryIds.Any())
            {
                productsQuery = productsQuery.Where(p =>
                    p.ProductCategories.Any(pc =>
                        request.Filter.CategoryIds.Contains(pc.CategoryId)));
            }

            if (request.Filter.OptionIds.Any())
            {
                productsQuery = productsQuery.Where(p =>
                    request.Filter.OptionIds.All(optionId =>
                        p.ProductOptions.Any(po =>
                            po.OptionId == optionId)));
            }

            if (request.Filter.OptionValueIds.Any())
            {
                productsQuery = productsQuery.Where(p =>
                    p.ProductVariants.Any(v =>
                        request.Filter.OptionValueIds.All(valueId =>
                            v.ProductVariantOptionValues.Any(x =>
                                x.OptionValueId == valueId))));
            }

            if (request.Filter.MinPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p =>
                    p.ProductVariants.Any(v =>
                       v.Price >= request.Filter.MinPrice));
            }

            if (request.Filter.MaxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p =>
                    p.ProductVariants.Any(v =>
                       v.Price <= request.Filter.MaxPrice));
            }

            productsQuery = request.Filter.Sort switch
            {
                ProductSort.PriceAscending => productsQuery.OrderBy(
                    p => p.ProductVariants.Min(v => v.Price)),

                ProductSort.PriceDescending => productsQuery.OrderByDescending(
                    p => p.ProductVariants.Max(v => v.Price)),

                ProductSort.Newest => productsQuery.OrderByDescending(
                    p => p.CreatedAt),

                _ => productsQuery.OrderBy(p => p.Id)
            };

            var totalCount = await productsQuery
                .CountAsync(ct);

            var products = await productsQuery
                .Skip((request.Filter.PageNumber - 1) * request.Filter.PageSize)
                .Take(request.Filter.PageSize)
                .ToProductDto()
                .ToListAsync(ct);

            return new PagedResult<ReadProductDto>
            {
                Items = products,
                TotalCount = totalCount,
                PageNumber = request.Filter.PageNumber,
                PageSize = request.Filter.PageSize
            };
        }
    }
}
