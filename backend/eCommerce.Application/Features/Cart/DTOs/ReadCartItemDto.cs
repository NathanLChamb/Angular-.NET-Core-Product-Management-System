namespace eCommerce.Application.Features.Cart.DTOs
{
    public class ReadCartItemDto
    {
        public int Id { get; set; }
        public int ProductVariantId { get; set; }

        // Optional, but usually useful
        public string ProductName { get; set; } = string.Empty;
        public List<ReadOptionValueFromCartDto> OptionValues { get; set; } = new();
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
