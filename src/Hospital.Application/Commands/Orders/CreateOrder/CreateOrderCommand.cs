using Hospital.Domain.Enums;
using MediatR;

namespace Hospital.Application.Commands.Orders.CreateOrder 
{
    public record CreateOrderCommand(
        string PatientId,
        string PatientName,
        string ServiceCode,
        string ServiceDescription,
        OrderPriority Priority
    ) : IRequest<CreateOrderResponse>;
}