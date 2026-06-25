using Hospital.Application.Interfaces;
using Hospital.Application.UnitOfWork;
using Hospital.Infrastructure.Persistence;

namespace Hospital.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HospitalDbContext _context;

        public UnitOfWork(
            HospitalDbContext context,
            IOrderRepository orderRepository)
        {
            _context = context;
            Orders = orderRepository;
        }

        public IOrderRepository Orders { get; }

        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
