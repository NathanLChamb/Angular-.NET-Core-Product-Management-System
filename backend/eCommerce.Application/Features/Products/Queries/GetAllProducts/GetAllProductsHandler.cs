using System;
using System.Collections.Generic;
using eCommerce.Application.Features.Products.DTOs;
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
            var query = _context.Products
                .AsNoTracking()
                .OrderBy(p => p.Id);

            var totalCount = await query
                .CountAsync(ct);

            var products = await query
                .Skip((request.PageParams.PageNumber - 1) * request.PageParams.PageSize)
                .Take(request.PageParams.PageSize)
                .ToProductDto()
                .ToListAsync(ct);

            return new PagedResult<ReadProductDto>
            {
                Items = products,
                TotalCount = totalCount,
                PageNumber = request.PageParams.PageNumber,
                PageSize = request.PageParams.PageSize
            };
        }
    }
}
