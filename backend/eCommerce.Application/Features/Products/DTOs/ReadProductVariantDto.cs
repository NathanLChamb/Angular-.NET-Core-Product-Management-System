using eCommerce.Application.Features.Options.DTOs;
using eCommerce.Application.Features.Products.Images.DTOs;

namespace eCommerce.Application.Features.Products.DTOs
{
    public class ReadProductVariantDto
    {
        public int Id { get; set; }
        public required string Sku { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public List<ReadOptionValueFromProductVariantDto> OptionValues { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
