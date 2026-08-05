namespace eCommerce.API.Contracts.Products;

public class AddProductImageRequest
{
    public string Url { get; set; } = null!;
    public int DisplayOrder { get; set; }
}