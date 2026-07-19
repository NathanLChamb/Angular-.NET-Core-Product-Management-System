using eCommerce.Application.Features.Auth.DTOs;
using eCommerce.Application.Interfaces;
using MediatR;

namespace eCommerce.Application.Features.Auth.Commands.Login;

public class LoginHandler
    : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;

    public LoginHandler(IIdentityService identityService, ITokenService tokenService)
    {
        _identityService = identityService;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken ct)
    {
        var user = await _identityService.LoginAsync(request.Email, request.Password);

        if (user == null) throw new Exception("Invalid credentials");

        var token = _tokenService.CreateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            DisplayName = user.DisplayName
        };
    }
}
