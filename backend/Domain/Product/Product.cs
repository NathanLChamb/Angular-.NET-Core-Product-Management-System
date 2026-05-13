namespace eCommercePractice4.Domain.Product
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public List<ProductVariant> ProductVariants { get; set; } = new();
        public List<ProductCategory> ProductCategories { get; set; } = new();
        public List<ProductOption> ProductOptions { get; set; } = new();
    }
}
