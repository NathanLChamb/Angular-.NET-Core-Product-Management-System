using eCommerce.Application.Features.Options.DTOs;
using eCommerce.Domain.Metadata;

namespace eCommerce.Application.Mapping
{
    public static class OptionProjection
    {
        public static IQueryable<ReadOptionDto> ToOptionDto(this IQueryable<Option> query)
        {
            return query.Select(o => new ReadOptionDto
            {
                Id = o.Id,
                Name = o.Name,
                OptionValues = o.OptionValues.Select(ov => new ReadOptionValueDto
                {
                    Id = ov.Id,
                    Value = ov.Value
                }).ToList()
            });
        }
    }
}
