namespace eCommerce.Application.Features.Categories.DTOs
{
    public class ReadCategoryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
