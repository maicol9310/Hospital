namespace Hospital.Domain.Orders
{
    public class OrderItem
    {
        public Guid Id { get; private set; }

        public Guid OrderId { get; private set; }

        public string Product { get; private set; } = string.Empty;

        public int Quantity { get; private set; }

        public decimal Price { get; private set; }

        public decimal SubTotal => Quantity * Price;

        private OrderItem() { }

        public OrderItem(
            Guid id,
            Guid orderId,
            string product,
            int quantity,
            decimal price)
        {
            Id = id;
            OrderId = orderId;
            Product = product;
            Quantity = quantity;
            Price = price;
        }
    }
}
