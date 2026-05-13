using eCommercePractice4.Domain.Metadata;

namespace eCommercePractice4.Domain.Product
{
    public class ProductCategory
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
