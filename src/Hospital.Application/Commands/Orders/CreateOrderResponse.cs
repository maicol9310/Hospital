using Hospital.Domain.Enums;

namespace Hospital.Application.Commands.Orders
{ 
    public sealed record CreateOrderResponse(
        Guid Id,
        decimal Total,
        OrderStatus Status
    );
}