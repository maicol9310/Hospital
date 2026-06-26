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

    public async Task<List<Order>?> GetByIdAsync(string patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
        .Where(o => o.PatientId == patientId)
        .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetAllAsync(
        string? patientId,
        OrderStatus? status,
        CancellationToken cancellationToken)
    {
        var query = _context.Orders.AsQueryable();

        if (!string.IsNullOrWhiteSpace(patientId))
            query = query.Where(x => x.PatientId == patientId);

        if (status.HasValue)
            query = query.Where(x => x.Status == status.Value);

        return await query.ToListAsync(cancellationToken);
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }
}