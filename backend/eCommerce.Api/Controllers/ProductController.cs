using eCommerce.Application.Features.Products.Commands.CreateProduct;
using eCommerce.Application.Features.Products.Commands.DeleteProduct;
using eCommerce.Application.Features.Products.Commands.UpdateProduct;
using eCommerce.Application.Features.Products.DTOs;
using eCommerce.Application.Features.Products.Filters;
using eCommerce.Application.Features.Products.Queries.GetAllProducts;
using eCommerce.Application.Features.Products.Queries.GetProductById;
using eCommerce.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadProductDto>>> GetAllProducts([FromQuery] ProductSearchFilter filter)
        {
            var products = await _mediator.Send(new GetAllProductsQuery(filter));
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadProductDto?>> GetProductById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<ReadProductDto>> CreateProduct([FromBody] CreateProductDto dto)
        {
            var product = await _mediator.Send(new CreateProductCommand(dto.Name, dto.Description, dto.CategoryIds, dto.OptionIds, dto.ProductVariants));
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            await _mediator.Send(new UpdateProductCommand(id, dto.Name, dto.Description, dto.CategoryIds, dto.OptionIds, dto.ProductVariants));
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}
