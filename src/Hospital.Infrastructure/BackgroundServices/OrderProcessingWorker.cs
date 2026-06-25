using Hospital.Domain.Orders;
using Hospital.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hospital.Infrastructure.BackgroundServices
{
    public class OrderProcessingWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OrderProcessingWorker> _logger;

        public OrderProcessingWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<OrderProcessingWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var db = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();

                var orders = await db.Orders
                    .Where(o => o.Status == OrderStatus.Pending)
                    .OrderBy(o => o.CreatedAt)
                    .ToListAsync(stoppingToken);

                foreach (var order in orders)
                {
                    _logger.LogInformation("Procesando {Id}", order.Id);

                    await Task.Delay(3000, stoppingToken);

                    order.MarkAsProcessed();

                    await db.SaveChangesAsync(stoppingToken);
                }

                await Task.Delay(TimeSpan.FromSeconds(40), stoppingToken);
            }
        }
    }
}
