using eCommerce.Application.Features.Categories.DTOs;
using eCommerce.Application.Features.Categories.Mappings;
using eCommerce.Application.Interfaces;
using eCommerce.Application.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, PagedResult<ReadCategoryDto>>
    {
        private readonly IeCommerceContext _context;
        public GetAllCategoriesHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ReadCategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken ct)
        {
            var query = _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Id);

            var totalCount = await query
                .CountAsync();

            var categories = await query
                .Skip((request.pageParams.PageNumber - 1) * request.pageParams.PageSize)
                .Take(request.pageParams.PageSize)
                .ToCategoryDto()
                .ToListAsync();

            return new PagedResult<ReadCategoryDto>
            {
                Items = categories,
                TotalCount = totalCount,
                PageNumber = request.pageParams.PageNumber,
                PageSize = request.pageParams.PageSize
            };
        }
    }
}
