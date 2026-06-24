using MediatR;

namespace Hospital.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    string CustomerName,
    IReadOnlyCollection<CreateOrderItemDto> Items
) : IRequest<CreateOrderResponse>;

public record CreateOrderItemDto(
    string Product,
    int Quantity,
    decimal Price
);