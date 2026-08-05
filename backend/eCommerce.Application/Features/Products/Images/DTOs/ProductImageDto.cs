namespace eCommerce.Application.Features.Products.Images.DTOs;

public class ProductImageDto
{
    public int Id { get; set; }
    public string Url { get; set; } = null!;
    public int DisplayOrder { get; set; }
}
