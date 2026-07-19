namespace eCommerce.Application.Features.Auth.Models;

public class TokenUser
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string DisplayName { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}
