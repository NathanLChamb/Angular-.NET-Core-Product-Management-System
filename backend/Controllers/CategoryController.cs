using eCommercePractice4.Application.DTOs.Category;
using eCommercePractice4.Application.Interfaces;
using eCommercePractice4.Application.Shared;
using Microsoft.AspNetCore.Mvc;

namespace eCommercePractice4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadCategoryDto>>> GetAllCategories([FromQuery]PaginationParams pageParams)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(pageParams);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCategoryDto?>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryDto dto)
        {
            var newCategory = await _categoryService.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, newCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto dto)
        {
            await _categoryService.UpdateCategoryAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
