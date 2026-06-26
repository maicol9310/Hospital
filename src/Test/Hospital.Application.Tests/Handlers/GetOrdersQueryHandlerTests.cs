using AutoMapper;
using FluentAssertions;
using Hospital.Application.DTOs.Order;
using Hospital.Application.Queries.Orders.GetOrders;
using Hospital.Application.UnitOfWork;
using Hospital.Domain.Orders;
using Moq;

namespace Hospital.Application.Tests.Handlers
{
    [TestFixture]
    public class GetOrdersQueryHandlerTests
    {
        private Mock<IUnitOfWork> _uow = null!;
        private Mock<IMapper> _mapper = null!;
        private GetOrdersQueryHandler _handler = null!;

        [SetUp]
        public void Setup()
        {
            _uow = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();

            _handler = new GetOrdersQueryHandler(
                _uow.Object,
                _mapper.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnOrders()
        {
            var orders = new List<Order>();

            var expected = new List<OrderDto>
            {
                new OrderDto(
                    Guid.NewGuid(),
                    "123",
                    "Juan",
                    "LAB",
                    "Laboratorio",
                    OrderPriority.Normal,
                    OrderStatus.Pending,
                    DateTime.UtcNow,
                    null)
            };

            _uow.Setup(x =>
                    x.Orders.GetAllAsync(
                        It.IsAny<string?>(),
                        It.IsAny<OrderStatus?>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(orders);

            _mapper.Setup(x =>
                    x.Map<IEnumerable<OrderDto>>(It.IsAny<List<Order>>()))
                .Returns(expected);

            var result = await _handler.Handle(
                new GetOrdersQuery(null, null),
                CancellationToken.None);

            result.Should().BeEquivalentTo(expected);
        }
    }
}