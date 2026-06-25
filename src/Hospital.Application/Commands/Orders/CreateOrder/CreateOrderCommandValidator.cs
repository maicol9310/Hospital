using FluentValidation;

namespace Hospital.Application.Commands.Orders.CreateOrder
{
    public class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty();

            RuleFor(x => x.ServiceCode)
                .NotEmpty();

            RuleFor(x => x.Priority)
                .IsInEnum();
        }
    }
}