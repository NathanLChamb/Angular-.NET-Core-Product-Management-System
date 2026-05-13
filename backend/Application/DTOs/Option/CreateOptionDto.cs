using System.ComponentModel.DataAnnotations;

namespace eCommercePractice4.Application.DTOs.Option
{
    public class CreateOptionDto
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [Required]
        [MinLength(1)]
        public List<string> OptionValues { get; set; } = new();

    }
}
