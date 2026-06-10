using eCommerce.Domain.Product;

namespace eCommerce.Domain.Metadata
{
    public class Option
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<OptionValue> OptionValues { get; set; } = new();
        public List<ProductOption> ProductOptions { get; set; } = new();
    }
}
