using eCommercePractice4.Application.DTOs.Option;
using eCommercePractice4.Application.Interfaces;
using eCommercePractice4.Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommercePractice4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IOptionService _optionService;
        public OptionController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadOptionDto>>> GetAllOptions([FromQuery]PaginationParams pageParams)
        {
            var options = await _optionService.GetAllOptionsAsync(pageParams);
            return Ok(options);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadOptionDto?>> GetOptionById(int id)
        {
            var option = await _optionService.GetOptionByIdAsync(id);
            return Ok(option);
        }

        [HttpPost]
        public async Task<ActionResult<ReadOptionDto>> CreateOption([FromBody]CreateOptionDto dto)
        {
            var option = await _optionService.CreateOptionAsync(dto);
            return CreatedAtAction(nameof(GetOptionById), new { id = option.Id }, option);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOption(int id, UpdateOptionDto dto)
        {
            await _optionService.UpdateOptionAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            await _optionService.DeleteOptionAsync(id);
            return NoContent();
        }
    }
}
