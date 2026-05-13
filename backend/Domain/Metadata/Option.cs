using eCommercePractice4.Domain.Product;

namespace eCommercePractice4.Domain.Metadata
{
    public class Option
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<OptionValue> OptionValues { get; set; } = new();
        public List<ProductOption> ProductOptions { get; set; } = new();
    }
}