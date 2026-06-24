using Hospital.Domain.Enums;

namespace Hospital.Application.Commands.Orders.CreateOrder
{ 
    public record CreateOrderResponse(
        Guid Id,
        decimal Total,
        OrderStatus Status
    );
}