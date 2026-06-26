using Hospital.Application.DTOs.Login;
using MediatR;

namespace Hospital.Application.Commands.Login
{
    public record LoginCommand(string Username, string Password) : IRequest<AuthDto>;
}