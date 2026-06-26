using FluentValidation;
using Hospital.Application.UnitOfWork;

namespace Hospital.Application.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(x => x)
                .MustAsync(async (cmd, ct) =>
                {
                    var user = await _unitOfWork.Users.GetByUsernameAsync(cmd.Username, ct);
                    return user != null;
                })
                .WithMessage("Incorrect username or password")
                .When(x => !string.IsNullOrWhiteSpace(x.Username)); 

            RuleFor(x => x)
                .MustAsync(async (cmd, ct) =>
                {
                    var user = await _unitOfWork.Users.GetByUsernameAsync(cmd.Username, ct);
                    return user != null && user.PasswordHash == cmd.Password;
                })
                .WithMessage("Incorrect username or password")
                .When(x => !string.IsNullOrWhiteSpace(x.Username) && !string.IsNullOrWhiteSpace(x.Password));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
