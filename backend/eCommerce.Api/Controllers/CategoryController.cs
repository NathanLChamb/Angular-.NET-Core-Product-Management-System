using eCommerce.Application.DTOs.Category;
using eCommerce.Application.Interfaces;
using eCommerce.Application.Shared;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers
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
        public async Task<ActionResult<ReadCategoryDto>> CreateCategory([FromBody]CreateCategoryDto dto)
        {
            var category = await _categoryService.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
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
