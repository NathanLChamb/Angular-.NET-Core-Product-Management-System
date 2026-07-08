using eCommerce.Application.Features.Categories.Commands.CreateCategory;
using eCommerce.Application.Features.Categories.Commands.DeleteCategory;
using eCommerce.Application.Features.Categories.Commands.UpdateCategory;
using eCommerce.Application.Features.Categories.DTOs;
using eCommerce.Application.Features.Categories.Queries.GetAllCategories;
using eCommerce.Application.Features.Categories.Queries.GetCategoryById;
using eCommerce.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadCategoryDto>>> GetAllCategories([FromQuery]PaginationParams pageParams)
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery(pageParams));
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCategoryDto?>> GetCategoryById(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult<ReadCategoryDto>> CreateCategory([FromBody]CreateCategoryDto dto)
        {
            var category = await _mediator.Send(new CreateCategoryCommand(dto.Name, dto.Description));
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto dto)
        {
            await _mediator.Send(new UpdateCategoryCommand(id, dto.Name, dto.Description));
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }
    }
}
