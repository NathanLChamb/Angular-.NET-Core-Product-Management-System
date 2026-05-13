using System.ComponentModel.DataAnnotations;

namespace eCommercePractice4.Application.DTOs.Option
{
    public class UpdateOptionValueDto
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Value { get; set; }
    }
}
