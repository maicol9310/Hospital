using AutoMapper;
using FluentAssertions;
using Hospital.Application.DTOs.Order;
using Hospital.Application.Queries.Orders.GetOrder;
using Hospital.Application.UnitOfWork;
using Hospital.Domain.Orders;
using Moq;

namespace Hospital.Application.Tests.Handlers
{
    [TestFixture]
    public class GetOrderQueryHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWork = null!;
        private Mock<IMapper> _mapper = null!;
        private GetOrderQueryHandler _handler = null!;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();

            _handler = new GetOrderQueryHandler(
                _unitOfWork.Object,
                _mapper.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnOrders()
        {
            // Arrange
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

            _unitOfWork.Setup(x =>
                    x.Orders.GetByIdAsync(
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(orders);

            _mapper.Setup(x =>
                    x.Map<List<OrderDto>>(orders))
                .Returns(expected);

            // Act
            var result = await _handler.Handle(
                new GetOrderQuery("123"),
                CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task Handle_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            _unitOfWork.Setup(x =>
                    x.Orders.GetByIdAsync(
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Order>?)null);

            // Act
            var result = await _handler.Handle(
                new GetOrderQuery("123"),
                CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}