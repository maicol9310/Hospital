using Hospital.Domain.Enums;

namespace Hospital.Application.Commands.Orders.CreateOrder
{
    public record CreateOrderResponse(
        Guid Id,
        string PatientId,
        string PatientName,
        string ServiceCode,
        string ServiceDescription,
        OrderPriority Priority,
        OrderStatus Status,
        DateTime CreatedAt
    );
}