namespace eCommerce.Api.Contracts.Products
{
    public class AddProductOptionValueImageRequest
    {
        public required string Url { get; set; }
        public bool IsDefault { get; set; }
        public List<int> OptionValueIds { get; set; } = new();
    }
}
