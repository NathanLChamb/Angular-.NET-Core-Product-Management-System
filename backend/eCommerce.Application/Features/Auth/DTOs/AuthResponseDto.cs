namespace eCommerce.Application.Features.Auth.DTOs;

public class AuthResponseDto
{
    public required string Token { get; set; }
    public required string Email { get; set; }
    public string? DisplayName { get; set; }
}
