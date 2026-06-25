namespace Hospital.Domain.Orders;

public class Order
{
    public Guid Id { get; private set; }

    public string PatientId { get; private set; } = string.Empty;

    public string PatientName { get; private set; } = string.Empty;

    public string ServiceCode { get; private set; } = string.Empty;

    public string ServiceDescription { get; private set; } = string.Empty;

    public OrderPriority Priority { get; private set; }

    public OrderStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? ProcessedAt { get; private set; }

    private Order() { }

    public Order(
        string patientId,
        string patientName,
        string serviceCode,
        string serviceDescription,
        OrderPriority priority)
    {
        Id = Guid.NewGuid();

        if (string.IsNullOrWhiteSpace(patientId))
            throw new ArgumentException("PatientId es obligatorio.", nameof(patientId));

        if (string.IsNullOrWhiteSpace(serviceCode))
            throw new ArgumentException("ServiceCode es obligatorio.", nameof(serviceCode));

        PatientId = patientId;
        PatientName = patientName;
        ServiceCode = serviceCode;
        ServiceDescription = serviceDescription;
        Priority = priority;

        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsProcessed()
    {
        if (Status == OrderStatus.Processed)
            return;

        Status = OrderStatus.Processed;
        ProcessedAt = DateTime.UtcNow;
    }

}