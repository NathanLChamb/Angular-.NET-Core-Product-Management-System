using eCommerce.Application.DTOs.Category;
using eCommerce.Domain.Metadata;

namespace eCommerce.Application.Mapping
{
    public static class CategoryProjection
    {
        public static IQueryable<ReadCategoryDto> ToCategoryDto(this IQueryable<Category> query)
        {
            return query.Select(c => new ReadCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            });
        }
    }
}
