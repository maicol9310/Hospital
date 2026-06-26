using Hospital.Domain.Orders;

namespace Hospital.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken);

        Task<Order?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Order>> GetAllAsync(string? patientId, OrderStatus? status, CancellationToken cancellationToken);

    }
}