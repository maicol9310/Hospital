using FluentValidation;

namespace Hospital.Application.Queries.Orders.GetOrderById
{
    public class GetOrderByIdQueryValidator: AbstractValidator <GetOrderByIdQuery>
    {
       public GetOrderByIdQueryValidator() 
       {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
