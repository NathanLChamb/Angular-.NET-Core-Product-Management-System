using eCommerce.Application.DTOs.Category;
using eCommerce.Application.Shared;

namespace eCommerce.Application.Interfaces
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
