namespace eCommerce.Application.Features.Products.DTOs
{
    public class CreateProductDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<int> CategoryIds { get; set; } = new();
        public List<int> OptionIds { get; set; } = new();
        public List<CreateProductVariantDto> ProductVariants { get; set; } = new();
    }
}
