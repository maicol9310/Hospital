using Hospital.Domain.Enums;

namespace Hospital.Application.DTOs.Order
{
    public record OrderDto(
        Guid Id,
        string CustomerName,
        DateTime CreatedAt,
        DateTime? ProcessedAt,
        decimal Total,
        OrderStatus Status,
        IReadOnlyCollection<OrderItemDto> Items
    );
}
