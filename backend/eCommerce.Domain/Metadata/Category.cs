using eCommerce.Domain.Product;

namespace eCommerce.Domain.Metadata
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<ProductCategory> ProductCategories { get; set; } = new();
    }
}
