using eCommercePractice4.Application.DTOs.Option;
using eCommercePractice4.Domain.Metadata;

namespace eCommercePractice4.Application.DTOs.Product
{
    public class ReadProductVariantDto
    {
        public int Id { get; set; }
        public required string Sku { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public List<ReadOptionValueDto> OptionValues { get; set; } = new();
    }
}
