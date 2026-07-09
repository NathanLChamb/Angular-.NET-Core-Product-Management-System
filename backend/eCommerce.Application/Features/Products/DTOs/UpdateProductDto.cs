using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.Features.Products.DTOs
{
    public class UpdateProductDto
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [Required]
        [StringLength(300)]
        public required string Description { get; set; }
        [Required]
        [MinLength(1)]
        public List<int> CategoryIds { get; set; } = new();
        [Required]
        [MinLength(1)]
        public List<int> OptionIds { get; set; } = new();
        [Required]
        [MinLength(1)]
        public List<UpdateProductVariantDto> ProductVariants { get; set; } = new();
    }
}
