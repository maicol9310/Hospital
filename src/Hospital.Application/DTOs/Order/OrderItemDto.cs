namespace Hospital.Application.DTOs.Order
{
    public record OrderItemDto(
        Guid Id,
        string Product,
        int Quantity,
        decimal Price,
        decimal SubTotal
    );
}
