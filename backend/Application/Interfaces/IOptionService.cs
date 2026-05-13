using eCommercePractice4.Application.DTOs.Option;
using eCommercePractice4.Application.Shared;

namespace eCommercePractice4.Application.Interfaces
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
