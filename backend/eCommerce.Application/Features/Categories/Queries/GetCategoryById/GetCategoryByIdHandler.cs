using eCommerce.Application.Exceptions;
using eCommerce.Application.Features.Categories.DTOs;
using eCommerce.Application.Features.Categories.Mappings;
using eCommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, ReadCategoryDto>
    {
        private readonly IeCommerceContext _context;
        public GetCategoryByIdHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<ReadCategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken ct)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .Where(c => c.Id == request.Id)
                .ToCategoryDto()
                .FirstOrDefaultAsync();
            if (category == null) throw new NotFoundException("Category not found from provided ID");

            return category;
        }
    }
}
