using Hospital.Domain.Enums;

namespace Hospital.Application.Orders.Commands.CreateOrder;

public sealed record CreateOrderResponse(
    Guid Id,
    decimal Total,
    OrderStatus Status
);