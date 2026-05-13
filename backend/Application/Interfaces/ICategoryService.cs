using eCommercePractice4.Application.DTOs.Category;
using eCommercePractice4.Application.Shared;
using eCommercePractice4.Infrastructure;

namespace eCommercePractice4.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<PagedResult<ReadCategoryDto>> GetAllCategoriesAsync(PaginationParams pageParams);
        public Task<ReadCategoryDto?> GetCategoryByIdAsync(int id);
        public Task<ReadCategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
        public Task UpdateCategoryAsync(int id, UpdateCategoryDto dto);
        public Task DeleteCategoryAsync(int id);
    }
}
