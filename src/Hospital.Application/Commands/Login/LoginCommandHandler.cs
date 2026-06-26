using Hospital.Application.DTOs.Login;
using Hospital.Application.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hospital.Application.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IConfiguration config, ILogger<LoginCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;
        }

        public async Task<AuthDto> Handle(LoginCommand request, CancellationToken ct)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(request.Username, ct);

            if (user is null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                ),

                Audience = _config["Jwt:Audience"],
                Issuer = _config["Jwt:Issuer"]
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return new AuthDto
            {
                Username = user.Username,
                PasswordHash = "*****",
                Token = handler.WriteToken(token)
            };
        }

    }
}