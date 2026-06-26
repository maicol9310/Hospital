using Hospital.Application.Interfaces;

namespace Hospital.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}