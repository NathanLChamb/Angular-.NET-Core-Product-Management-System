using System.ComponentModel.DataAnnotations;

namespace eCommercePractice4.Application.DTOs.Product
{
    public class CreateProductVariantDto
    {
        [Required]
        [StringLength(50)]
        public required string Sku { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        [Required]
        [MinLength(1)]
        public List<int> OptionValueIds { get; set; } = new();
    }
}
