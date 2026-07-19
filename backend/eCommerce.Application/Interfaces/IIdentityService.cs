using eCommerce.Application.Features.Auth.Models;

public interface IIdentityService
{
    Task<TokenUser?> LoginAsync(
        string email,
        string password);

    Task<TokenUser> RegisterAsync(
        string email,
        string password,
        string displayName);
}