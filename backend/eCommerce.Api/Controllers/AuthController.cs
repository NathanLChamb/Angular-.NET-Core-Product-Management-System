using eCommerce.Application.Features.Auth.Commands.Login;
using eCommerce.Application.Features.Auth.Commands.Register;
using eCommerce.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
        {
            var response = await _mediator.Send(new RegisterCommand(dto.Email, dto.Password, dto.DisplayName));

            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto dto)
        {
            var response = await _mediator.Send(new LoginCommand(dto.Email, dto.Password));

            return Ok(response);
        }
    }
}
