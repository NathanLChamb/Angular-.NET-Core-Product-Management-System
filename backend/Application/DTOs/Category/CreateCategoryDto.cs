using System.ComponentModel.DataAnnotations;

namespace eCommercePractice4.Application.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [Required]
        [StringLength(300)]
        public required string Description { get; set; }
    }
}