using Hospital.Application.DTOs.Order;
using Hospital.Domain.Orders;
using MediatR;

namespace Hospital.Application.Queries.Orders.GetOrders
{
    public record GetOrdersQuery(
        string? PatientId,
        OrderStatus? Status) : IRequest<IEnumerable<OrderDto>>;
}