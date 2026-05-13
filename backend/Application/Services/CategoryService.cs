using eCommercePractice4.Application.DTOs.Category;
using eCommercePractice4.Application.Exceptions;
using eCommercePractice4.Application.Interfaces;
using eCommercePractice4.Application.Shared;
using eCommercePractice4.Domain.Metadata;
using eCommercePractice4.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace eCommercePractice4.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly eCommercePracticeContext _context;
        public CategoryService(eCommercePracticeContext context)
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

            var items = await query
                .Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize)
                .Select(c => new ReadCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();

            return new PagedResult<ReadCategoryDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageParams.PageNumber,
                PageSize = pageParams.PageSize
            };
        }

        public async Task<ReadCategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new ReadCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .FirstOrDefaultAsync();
            if (category == null) throw new NotFoundException("Category not found from provided ID");

            return category;
        }

        public async Task<ReadCategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var newCategory = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return new ReadCategoryDto
            {
                Id = newCategory.Id,
                Name = newCategory.Name,
                Description = newCategory.Description
            };
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new NotFoundException("Id for update not found from provided ID");

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new NotFoundException("Id for update not found from provided ID");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
