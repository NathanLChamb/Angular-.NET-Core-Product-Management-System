using eCommerce.Domain.Metadata;

namespace eCommerce.Domain.Product
{
    public class ProductOption
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int OptionId { get; set; }
        public Option Option { get; set; } = null!;
    }
}
