using Hospital.Domain.Enums;

namespace Hopsital.Domain.Orders;

public class Order
{
    public Guid Id { get; private set; }

    public string CustomerName { get; private set; } = string.Empty;

    public decimal Total { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public OrderStatus Status { get; private set; }

    private Order() { }

    public Order(string customerName, decimal total)
    {
        Id = Guid.NewGuid();
        CustomerName = customerName;
        Total = total;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    public void MarkAsProcessed()
    {
        Status = OrderStatus.Processed;
    }
}