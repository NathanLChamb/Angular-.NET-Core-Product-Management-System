using eCommerce.Application.Features.Auth.DTOs;
using MediatR;

namespace eCommerce.Application.Features.Auth.Commands.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<AuthResponseDto>;
