using Hospital.Domain.Enums;

namespace Hospital.Application.DTOs.Order
{
    public record OrderDto(
        Guid Id,
        string PatientId,
        string? PatientName,
        string ServiceCode,
        string? ServiceDescription,
        OrderPriority Priority,
        OrderStatus Status,
        DateTime CreatedAt,
        DateTime? ProcessedAt
    );
}
