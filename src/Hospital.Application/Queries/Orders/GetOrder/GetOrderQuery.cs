using Hospital.Application.DTOs.Order;
using MediatR;

namespace Hospital.Application.Queries.Orders.GetOrder
{
    public record GetOrderQuery(string Id) : IRequest<OrderDto?>;
}
