using Hospital.Application.Interfaces;

namespace Hospital.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}