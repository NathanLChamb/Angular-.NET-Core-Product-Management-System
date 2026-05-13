using eCommercePractice4.Application.DTOs.Category;
using eCommercePractice4.Application.DTOs.Option;

namespace eCommercePractice4.Application.DTOs.Product
{
    public class ReadProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<ReadCategoryDto> Categories { get; set; } = new();
        public List<ReadOptionFromProductDto> Options { get; set; } = new();
        public List<ReadProductVariantDto> ProductVariants { get; set; } = new();
    }
}
