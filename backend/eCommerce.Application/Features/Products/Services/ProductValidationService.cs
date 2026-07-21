using eCommerce.Application.Exceptions;
using eCommerce.Application.Features.Products.Interfaces;
using eCommerce.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Products.Services
{
    public class ProductValidationService : IProductValidationService
    {
        private readonly IeCommerceContext _context;

        public ProductValidationService(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task ValidateCategoriesAsync(List<int> categoryIds, CancellationToken ct)
        {
            var existingIds = await _context.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .Select(c => c.Id)
                .ToListAsync(ct);

            var missingIds = categoryIds.Except(existingIds);
            if (missingIds.Any())
            {
                throw new BusinessRuleException($"Categories not found: {string.Join(", ", missingIds)}");
            }
        }

        public async Task ValidateOptionsAsync(List<int> optionIds, CancellationToken ct)
        {
            var existingIds = await _context.Options
                .Where(o => optionIds.Contains(o.Id))
                .Select(o => o.Id)
                .ToListAsync(ct);

            var missingIds = optionIds.Except(existingIds);
            if (missingIds.Any())
            {
                throw new BusinessRuleException($"Options not found: {string.Join(", ", missingIds)}");
            }
        }

        public async Task ValidateOptionValuesAsync(List<int> optionValueIds, CancellationToken ct)
        {
            var existingIds = await _context.OptionValues
                .Where(ov => optionValueIds.Contains(ov.Id))
                .Select(ov => ov.Id)
                .ToListAsync(ct);

            var missingIds = optionValueIds.Except(existingIds);
            if (missingIds.Any())
            {
                throw new BusinessRuleException($"Option values not found: {string.Join(", ", missingIds)}");
            }
        }

        public async Task ValidateOptionValuesBelongToOptionsAsync(List<int> optionIds, List<int> optionValueIds, CancellationToken ct)
        {
            var invalidValues = await _context.OptionValues
                .Where(ov => optionValueIds.Contains(ov.Id))
                .Where(ov => !optionIds.Contains(ov.OptionId))
                .Select(ov => ov.Id)
                .ToListAsync(ct);

            if (invalidValues.Any()) throw new BusinessRuleException("Option values do not match product options");
        }
    }
}
