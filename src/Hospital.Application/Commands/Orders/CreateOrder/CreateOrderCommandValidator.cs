using FluentValidation;

namespace Hospital.Application.Commands.Orders.CreateOrder
{
    public class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.PatientId)
                .Must(x => x != "string")
                .WithMessage("Invalid test value")
                .NotEmpty();

            RuleFor(x => x.PatientName)
                .Must(x => x != "string")
                .WithMessage("Invalid test value")
                .NotEmpty();

            RuleFor(x => x.ServiceCode)
                .Must(x => x != "string")
                .WithMessage("Invalid test value")
                .NotEmpty();

            RuleFor(x => x.Priority)
                .IsInEnum();
        }
    }
}