namespace eCommerce.Application.Features.Cart.DTOs
{
    public class ReadCartDto
    {
        public int Id { get; set; }
        public List<ReadCartItemDto> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
    }
}
