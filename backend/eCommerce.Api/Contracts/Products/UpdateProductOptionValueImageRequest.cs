namespace eCommerce.Api.Contracts.Products
{
    public class UpdateProductOptionValueImageRequest
    {
        public string Url { get; set; } = null!;
        public bool IsDefault { get; set; }
        public List<int> OptionValueIds { get; set; } = new();
    }
}
