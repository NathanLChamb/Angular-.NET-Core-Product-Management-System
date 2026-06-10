using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Shared;

namespace eCommerce.Application.Interfaces
{
    public interface IProductService
    {
        public Task<PagedResult<ReadProductDto>> GetAllProductsAsync(PaginationParams pageParams);
        public Task<ReadProductDto?> GetProductByIdAsync(int id);
        public Task<ReadProductDto> CreateProductAsync(CreateProductDto dto);
        public Task UpdateProductAsync(int id, UpdateProductDto dto);
        public Task DeleteProductAsync(int id);
    }
}
