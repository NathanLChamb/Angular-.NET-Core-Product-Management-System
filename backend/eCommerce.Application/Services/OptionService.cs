using eCommerce.Application.Exceptions;
using eCommerce.Application.Features.Options.DTOs;
using eCommerce.Application.Interfaces;
using eCommerce.Application.Mapping;
using eCommerce.Application.Shared;
using eCommerce.Domain.Metadata;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Services
{
    public class OptionService : IOptionService
    {
        private readonly IeCommerceContext _context;
        public OptionService(IeCommerceContext context)
        {
            _context = context;
        }
        public async Task<PagedResult<ReadOptionDto>> GetAllOptionsAsync(PaginationParams pageParams)
        {
            var query = _context.Options
                .AsNoTracking()
                .OrderBy(o => o.Id);

            var totalCount = await query
                .CountAsync();

            var options = await query
                .Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize)
                .ToOptionDto()
                .ToListAsync();

            return new PagedResult<ReadOptionDto>
            {
                Items = options,
                TotalCount = totalCount,
                PageNumber = pageParams.PageNumber,
                PageSize = pageParams.PageSize
            };
        }

        public async Task<ReadOptionDto?> GetOptionByIdAsync(int id)
        {
            var option = await _context.Options
                .AsNoTracking()
                .Where(o => o.Id == id)
                .ToOptionDto()
                .FirstOrDefaultAsync();
            if (option == null) throw new NotFoundException("Option not found from provided ID");

            return option;
        }
        public async Task<ReadOptionDto> CreateOptionAsync(CreateOptionDto dto)
        {
            var option = new Option
            {
                Name = dto.Name,
                OptionValues = dto.OptionValues.Select(value => new OptionValue
                {
                    Value = value
                }).ToList()
            };

            _context.Options.Add(option);
            await _context.SaveChangesAsync();

            return new ReadOptionDto
            {
                Id = option.Id,
                Name = option.Name,
                OptionValues = option.OptionValues.Select(ov => new ReadOptionValueDto
                {
                    Id = ov.Id,
                    Value = ov.Value
                }).ToList()
            };
        }
        public async Task UpdateOptionAsync(int id, UpdateOptionDto dto)
        {
            var option = await _context.Options
                .Include(o => o.OptionValues)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (option == null) throw new NotFoundException("Option for update not found from provided ID");

            option.Name = dto.Name;

            option.OptionValues.RemoveAll(existing => !dto.OptionValues.Any(_dto => _dto.Id == existing.Id));
            option.OptionValues.AddRange(dto.OptionValues
                .Where(_dto => !_dto.Id.HasValue)
                .Select(_dto => new OptionValue
                {
                    Value = _dto.Value
                })
            );
            foreach (var existingValue in option.OptionValues)
            {
                var dtoValue = dto.OptionValues
                    .FirstOrDefault(_dto => _dto.Id == existingValue.Id);

                if (dtoValue != null && dtoValue.Value != existingValue.Value)
                {
                    existingValue.Value = dtoValue.Value;
                }
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteOptionAsync(int id)
        {
            var option = await _context.Options.FindAsync(id);
            if (option == null) throw new NotFoundException("Option for deletion not found from provided ID");

            _context.Options.Remove(option);
            await _context.SaveChangesAsync();
        } 
    }
}
