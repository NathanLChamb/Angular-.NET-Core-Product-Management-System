using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Interfaces;
using eCommerce.Application.Shared;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadProductDto>>> GetAllProducts([FromQuery] PaginationParams pageParams)
        {
            var products = await _productService.GetAllProductsAsync(pageParams);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadProductDto?>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<ReadProductDto>> CreateProduct([FromBody] CreateProductDto dto)
        {
            var product = await _productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            await _productService.UpdateProductAsync(id, dto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
