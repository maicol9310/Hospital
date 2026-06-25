using Hospital.Application.Interfaces;
using Hospital.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly HospitalDbContext _context;

    public OrderRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Orders.OrderBy(o => o.CreatedAt).ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(o => o.PatientId == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Where(o => o.Status == OrderStatus.Pending)
            .OrderBy(o => o.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }
}