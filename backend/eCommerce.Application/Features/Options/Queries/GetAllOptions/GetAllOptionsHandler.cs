using eCommerce.Application.Features.Options.DTOs;
using eCommerce.Application.Interfaces;
using eCommerce.Application.Mapping;
using eCommerce.Application.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Options.Queries.GetAllOptions
{
    public class GetAllOptionsHandler : IRequestHandler<GetAllOptionsQuery, PagedResult<ReadOptionDto>>
    {
        private readonly IeCommerceContext _context;
        public GetAllOptionsHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ReadOptionDto>> Handle(GetAllOptionsQuery request, CancellationToken ct)
        {
            var query = _context.Options
                .AsNoTracking()
                .OrderBy(o => o.Id);

            var totalCount = await query
                .CountAsync(ct);

            var options = await query
                .Skip((request.pageParams.PageNumber - 1) * request.pageParams.PageSize)
                .Take(request.pageParams.PageSize)
                .ToOptionDto()
                .ToListAsync(ct);

            return new PagedResult<ReadOptionDto>
            {
                Items = options,
                TotalCount = totalCount,
                PageNumber = request.pageParams.PageNumber,
                PageSize = request.pageParams.PageSize
            };
        }
    }
}
