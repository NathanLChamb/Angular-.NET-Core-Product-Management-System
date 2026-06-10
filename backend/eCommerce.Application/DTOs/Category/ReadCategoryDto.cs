namespace eCommerce.Application.DTOs.Category
{
    public class ReadCategoryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
