using FluentValidation;
using Hospital.Domain.Orders;

namespace Hospital.Application.Queries.Orders.GetOrders
{
    public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
    {
        public GetOrdersQueryValidator()
        {
            RuleFor(x => x.Status)
                .Must(status =>
                    !status.HasValue ||
                    status == OrderStatus.Pending ||
                    status == OrderStatus.Processed)
                .WithMessage("Status must be Pending or Processed.");
        }
    }
}
