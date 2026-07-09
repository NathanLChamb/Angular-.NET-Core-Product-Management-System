namespace eCommerce.Application.Features.Options.DTOs
{
    public class CreateOptionDto
    {
        public required string Name { get; set; }
        public List<string> OptionValues { get; set; } = new();
    }
}
