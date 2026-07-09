namespace eCommerce.Application.Features.Options.DTOs
{
    public class UpdateOptionDto
    {
        public required string Name { get; set; }
        public List<UpdateOptionValueDto> OptionValues { get; set; } = new();
    }
}
