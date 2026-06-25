using Hospital.Domain.Enums;

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

        Status = OrderStatus.Created;
        CreatedAt = DateTime.UtcNow;
    }

    public void Process()
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Solo una orden creada puede procesarse.");

        Status = OrderStatus.Processing;
    }

    public void Complete()
    {
        if (Status != OrderStatus.Processing)
            throw new InvalidOperationException("Solo una orden en proceso puede completarse.");

        Status = OrderStatus.Completed;
        ProcessedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Completed)
            throw new InvalidOperationException("No se puede cancelar una orden completada.");

        Status = OrderStatus.Cancelled;
    }
}