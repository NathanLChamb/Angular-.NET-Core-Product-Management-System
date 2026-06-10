using eCommerce.Application.DTOs.Category;

namespace eCommerce.Application.DTOs.Product
{
    public class ReadProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<ReadCategoryDto> Categories { get; set; } = new();
        public List<ReadOptionFromProductDto> Options { get; set; } = new();
        public List<ReadProductVariantDto> ProductVariants { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
