using eCommercePractice4.Domain.Product;

namespace eCommercePractice4.Domain.Metadata
{
    public class OptionValue
    {
        public int Id { get; set; }
        public required string Value { get; set; }
        public int OptionId { get; set; }
        public Option Option { get; set; } = null!;
        public List<ProductVariantOptionValue> ProductVariantOptionValues { get; set; } = new();
    }
}