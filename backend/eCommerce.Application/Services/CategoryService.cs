using eCommerce.Application.DTOs.Category;
using eCommerce.Application.Exceptions;
using eCommerce.Application.Interfaces;
using eCommerce.Application.Mapping;
using eCommerce.Application.Shared;
using eCommerce.Domain.Metadata;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IeCommerceContext _context;
        public CategoryService(IeCommerceContext context)
        {
            _context = context;
        }
        public async Task<PagedResult<ReadCategoryDto>> GetAllCategoriesAsync(PaginationParams pageParams)
        {
            var query = _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Id);

            var totalCount = await query
                .CountAsync();

            var categories = await query
                .Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize)
                .ToCategoryDto()
                .ToListAsync();

            return new PagedResult<ReadCategoryDto>
            {
                Items = categories,
                TotalCount = totalCount,
                PageNumber = pageParams.PageNumber,
                PageSize = pageParams.PageSize
            };
        }

        public async Task<ReadCategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ToCategoryDto()
                .FirstOrDefaultAsync();
            if (category == null) throw new NotFoundException("Category not found from provided ID");

            return category;
        }

        public async Task<ReadCategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new ReadCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
        public async Task UpdateCategoryAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new NotFoundException("Category for update not found from provided ID");

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new NotFoundException("Category for deletion not found from provided ID");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
