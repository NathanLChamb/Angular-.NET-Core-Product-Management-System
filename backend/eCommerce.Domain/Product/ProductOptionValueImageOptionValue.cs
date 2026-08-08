using eCommerce.Domain.Metadata;

namespace eCommerce.Domain.Product
{
    public class ProductOptionValueImageOptionValue
    {
        public int ProductOptionValueImageId { get; set; }
        public ProductOptionValueImage ProductOptionValueImage { get; set; } = new();
        public int OptionValueId { get; set; }
        public OptionValue OptionValue { get; set; } = null!;
    }
}
