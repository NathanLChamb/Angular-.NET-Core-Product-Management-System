using eCommercePractice4.Application.DTOs.Option;
using eCommercePractice4.Application.Exceptions;
using eCommercePractice4.Application.Interfaces;
using eCommercePractice4.Application.Shared;
using eCommercePractice4.Domain.Metadata;
using eCommercePractice4.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace eCommercePractice4.Application.Services
{
    public class OptionService : IOptionService
    {
        private eCommercePracticeContext _context;
        public OptionService(eCommercePracticeContext context)
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

            var items = await query
                .Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize)
                .Select(o => new ReadOptionDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    OptionValues = o.OptionValues.Select(ov => new ReadOptionValueDto
                    {
                        Id = ov.Id,
                        Value = ov.Value
                    }).ToList()
                })
                .ToListAsync();

            return new PagedResult<ReadOptionDto>
            {
                Items = items,
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
                .Select(o => new ReadOptionDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    OptionValues = o.OptionValues.Select(ov => new ReadOptionValueDto
                    {
                        Id = ov.Id,
                        Value = ov.Value
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            if (option == null) throw new NotFoundException("Option not found from provided ID");

            return option;
        }

        public async Task<ReadOptionDto> CreateOptionAsync(CreateOptionDto dto)
        {
            var newOption = new Option
            {
                Name = dto.Name,
                OptionValues = dto.OptionValues.Select(value => new OptionValue
                {
                    Value = value
                }).ToList()
            };

            _context.Options.Add(newOption);
            await _context.SaveChangesAsync();

            return await _context.Options
                .Where(o => o.Id == newOption.Id)
                .Select(o => new ReadOptionDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    OptionValues = o.OptionValues.Select(ov => new ReadOptionValueDto
                    {
                        Id = ov.Id,
                        Value = ov.Value
                    }).ToList()
                })
                .FirstAsync();
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
            if (option == null) throw new NotFoundException("Id for deletion not found from provided ID");

            _context.Options.Remove(option);
            await _context.SaveChangesAsync();    
        }
    }
}
