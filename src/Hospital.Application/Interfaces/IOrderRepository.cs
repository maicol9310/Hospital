using Hospital.Domain.Orders;

namespace Hospital.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken);

        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<Order>> GetPendingAsync(CancellationToken cancellationToken);

        void Update(Order order);
    }
}