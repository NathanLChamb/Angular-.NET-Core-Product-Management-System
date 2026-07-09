using eCommerce.Application.Features.Options.DTOs;
using eCommerce.Application.Interfaces;
using eCommerce.Domain.Metadata;
using MediatR;

namespace eCommerce.Application.Features.Options.Commands.CreateOption
{
    public class CreateOptionHandler : IRequestHandler<CreateOptionCommand, ReadOptionDto>
    {
        private readonly IeCommerceContext _context;
        public CreateOptionHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task<ReadOptionDto> Handle(CreateOptionCommand request, CancellationToken ct)
        {
            var newOption = new Option
            {
                Name = request.Name,
                OptionValues = request.OptionValues.Select(value => new OptionValue
                {
                    Value = value
                }).ToList()
            };

            _context.Options.Add(newOption);
            await _context.SaveChangesAsync(ct);

            return new ReadOptionDto
            {
                Id = newOption.Id,
                Name = newOption.Name,
                OptionValues = newOption.OptionValues.Select(ov => new ReadOptionValueDto
                {
                    Id = ov.Id,
                    Value = ov.Value
                }).ToList()
            };
        }
    }
}
