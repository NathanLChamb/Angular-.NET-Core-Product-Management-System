using eCommerce.Application.Exceptions;
using eCommerce.Application.Features.Options.DTOs;
using eCommerce.Application.Interfaces;
using eCommerce.Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Options.Queries.GetOptionById
{
    public class GetOptionByIdHandler : IRequestHandler<GetOptionByIdQuery, ReadOptionDto>
    {
        private readonly IeCommerceContext _context;
        public GetOptionByIdHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<ReadOptionDto> Handle(GetOptionByIdQuery request, CancellationToken ct)
        {
            var option = await _context.Options
                .Where(o => o.Id == request.Id)
                .ToOptionDto()
                .FirstOrDefaultAsync(ct);
            if (option == null) throw new NotFoundException("Option not found from provided ID");

            return option;
        }
    }
}
