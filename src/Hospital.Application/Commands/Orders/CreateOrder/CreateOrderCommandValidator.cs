using FluentValidation;

namespace Hospital.Application.Commands.Orders.CreateOrder
{ 
    public class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Items).NotNull().NotEmpty();
            RuleForEach(x => x.Items).SetValidator(new CreateOrderItemValidator());
        }
        private class CreateOrderItemValidator: AbstractValidator<CreateOrderItemDto>
        {
            public CreateOrderItemValidator()
            {
                RuleFor(x => x.Product).NotEmpty().MaximumLength(150);
                RuleFor(x => x.Quantity).GreaterThan(0);
                RuleFor(x => x.Price).GreaterThan(0);
            }
        }
    }
}