using eCommerce.Application.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminTestController : ControllerBase
{
    [HttpGet("test")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Test()
    {
        return Ok("You are an admin");
    }
}