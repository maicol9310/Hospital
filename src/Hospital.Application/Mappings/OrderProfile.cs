using AutoMapper;
using Hospital.Application.Commands.Orders.CreateOrder;
using Hospital.Application.DTOs.Order;
using Hospital.Domain.Orders;

namespace Hospital.Application.Mappings
{
    public sealed class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, CreateOrderResponse>();

            CreateMap<Order, OrderDto>();

            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
