using eCommercePractice4.Domain.Metadata;

namespace eCommercePractice4.Application.DTOs.Option
{
    public class ReadOptionDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<ReadOptionValueDto> OptionValues { get; set; } = new();
    }
}
