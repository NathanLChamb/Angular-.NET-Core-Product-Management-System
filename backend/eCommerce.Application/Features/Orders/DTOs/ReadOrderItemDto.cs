namespace eCommerce.Application.Features.Orders.DTOs
{
    public class ReadOrderItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public decimal PriceAtPurchase { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
