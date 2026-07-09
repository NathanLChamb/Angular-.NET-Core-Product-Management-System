using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.Features.Products.DTOs
{
    public class UpdateProductVariantDto
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Sku { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
        [Required]
        [MinLength(1)]
        public List<int> OptionValueIds { get; set; } = new();
    }
}
