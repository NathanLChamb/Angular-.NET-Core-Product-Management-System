using eCommerce.Application.Features.Auth.Models;

namespace eCommerce.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(TokenUser user);
}
