namespace eCommerce.Application.Features.Products.Images.DTOs
{
    public class ProductOptionValueImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public int DisplayOrder { get; set; }
        public bool IsDefault { get; set; }
        public List<int> OptionValueIds { get; set; } = [];
    }
}
