using eCommerce.Application.Features.Options.DTOs;
using eCommerce.Application.Shared;

namespace eCommerce.Application.Interfaces
{
    public interface IOptionService
    {
        public Task<PagedResult<ReadOptionDto>> GetAllOptionsAsync(PaginationParams pageParams);
        public Task<ReadOptionDto?> GetOptionByIdAsync(int id);
        public Task<ReadOptionDto> CreateOptionAsync(CreateOptionDto dto);
        public Task UpdateOptionAsync(int id, UpdateOptionDto dto);
        public Task DeleteOptionAsync(int id);
    }
}
