namespace eCommerce.Application.DTOs.Option
{
    public class ReadOptionDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<ReadOptionValueDto> OptionValues { get; set; } = new();
    }
}
