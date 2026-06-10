using eCommerce.Application.DTOs.Option;

namespace eCommerce.Application.DTOs.Product
{
    public class ReadProductVariantDto
    {
        public int Id { get; set; }
        public required string Sku { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public List<ReadOptionValueDto> OptionValues { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
