namespace eCommerce.Application.Features.Products.DTOs
{
    public class ReadOptionValueFromProductVariantDto
    {
        public int Id { get; set; }
        public required string Value { get; set; }
        public int OptionId { get; set; }
        public required string OptionName { get; set; }
    }
}
