namespace eCommercePractice4.Domain.Product
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public required string Sku { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public List<ProductVariantOptionValue> ProductVariantOptionValues { get; set; } = new();
    }
}