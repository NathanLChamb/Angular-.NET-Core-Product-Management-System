using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Option
{
    public class UpdateOptionDto
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [Required]
        [MinLength(1)]
        public List<UpdateOptionValueDto> OptionValues { get; set; } = new();
    }
}
