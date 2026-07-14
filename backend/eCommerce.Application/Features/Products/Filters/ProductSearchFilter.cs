namespace eCommerce.Application.Features.Products.Filters
{
    public class ProductSearchFilter
    {
        public string? Search { get; set; }
        public List<int> CategoryIds { get; set; } = new();
        public List<int> OptionIds { get; set; } = new();
        public ProductSort Sort { get; set; } = ProductSort.Default;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
