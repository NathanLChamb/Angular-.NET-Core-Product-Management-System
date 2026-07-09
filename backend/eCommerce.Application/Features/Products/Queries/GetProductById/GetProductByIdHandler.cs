using eCommerce.Application.Exceptions;
using eCommerce.Application.Features.Products.DTOs;
using eCommerce.Application.Features.Products.Mappings;
using eCommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ReadProductDto>
    {
        private readonly IeCommerceContext _context;
        public GetProductByIdHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<ReadProductDto> Handle(GetProductByIdQuery request, CancellationToken ct)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == request.Id)
                .ToProductDto()
                .FirstOrDefaultAsync(ct);
            if (product == null) throw new NotFoundException("Product not found from provided ID");

            return product;
        }
    }
}
