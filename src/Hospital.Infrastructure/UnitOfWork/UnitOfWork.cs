using Hospital.Application.Interfaces;
using Hospital.Application.UnitOfWork;
using Hospital.Infrastructure.Persistence;

namespace Hospital.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HospitalDbContext _context;
        public IOrderRepository Orders { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(
            HospitalDbContext context,
            IOrderRepository orderRepository,
            IUserRepository userRepository)
        {
            _context = context;
            Orders = orderRepository;
            Users = userRepository;
        }

        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
