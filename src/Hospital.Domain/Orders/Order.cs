using Hospital.Domain.Enums;
using Hospital.Domain.Orders;

public class Order
{
    public Guid Id { get; private set; }

    public string CustomerName { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public DateTime? ProcessedAt { get; private set; }

    public OrderStatus Status { get; private set; }

    private readonly List<OrderItem> _items = [];

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public decimal Total => _items.Sum(x => x.SubTotal);

    private Order() { }

    public Order(string customerName)
    {
        Id = Guid.NewGuid();
        CustomerName = customerName;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    public void AddItem(string product, int quantity, decimal price)
    {
        _items.Add(new OrderItem(
            Guid.NewGuid(),
            Id,
            product,
            quantity,
            price));
    }

    public void MarkAsProcessed()
    {
        Status = OrderStatus.Processed;
        ProcessedAt = DateTime.UtcNow;
    }
}