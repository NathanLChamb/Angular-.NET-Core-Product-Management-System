using eCommerce.Domain.Metadata;

namespace eCommerce.Domain.Product
{
    public class ProductVariantOptionValue
    {
        public int ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; } = null!;
        public int OptionValueId { get; set; }
        public OptionValue OptionValue { get; set; } = null!;
    }
}
