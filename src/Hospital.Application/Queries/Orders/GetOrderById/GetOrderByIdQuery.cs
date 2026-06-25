using Hospital.Application.DTOs.Order;
using MediatR;

namespace Hospital.Application.Queries.Orders.GetOrderById
{
    public record GetOrderByIdQuery(string Id) : IRequest<OrderDto?>;
}
