using eCommerce.Application.Features.Auth.DTOs;
using eCommerce.Application.Interfaces;
using MediatR;

namespace eCommerce.Application.Features.Auth.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;
        public RegisterHandler(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken ct)
        {
            var user = await _identityService.RegisterAsync(
                request.Email,
                request.Password,
                request.DisplayName);

            var token = _tokenService.CreateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                DisplayName = user.DisplayName
            };
        }
    }
}
