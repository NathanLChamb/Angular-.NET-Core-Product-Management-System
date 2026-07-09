using eCommerce.Application.Exceptions;
using eCommerce.Application.Interfaces;
using eCommerce.Domain.Metadata;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Options.Commands.UpdateOption
{
    public class UpdateOptionHandler : IRequestHandler<UpdateOptionCommand>
    {
        private readonly IeCommerceContext _context;
        public UpdateOptionHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateOptionCommand request, CancellationToken ct)
        {
            var option = await _context.Options
                .Include(o => o.OptionValues)
                .FirstOrDefaultAsync(o => o.Id == request.Id, ct);
            if (option == null) throw new NotFoundException("Option for update not found from provided ID");

            option.Name = request.Name ?? option.Name;
            
            option.OptionValues.RemoveAll(existing => !request.OptionValues.Any(_request => _request.Id == existing.Id));
            option.OptionValues.AddRange(request.OptionValues
                .Where(_request => !_request.Id.HasValue)
                .Select(_request => new OptionValue
                {
                    Value = _request.Value
                })
            );
            foreach (var existingValue in option.OptionValues)
            {
                var requestValue = request.OptionValues
                    .FirstOrDefault(dto => dto.Id == existingValue.Id);

                if (requestValue != null && requestValue.Value != existingValue.Value)
                {
                    existingValue.Value = requestValue.Value;
                }
            }

            await _context.SaveChangesAsync(ct);
        }
    }
}
