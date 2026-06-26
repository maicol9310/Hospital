using AutoMapper;
using FluentAssertions;
using Hospital.Application.DTOs.Order;
using Hospital.Application.Interfaces;
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
        private Mock<IOrderRepository> _orderRepository = null!;
        private GetOrderQueryHandler _handler = null!;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _orderRepository = new Mock<IOrderRepository>();
            _mapper = new Mock<IMapper>();

            _unitOfWork
                .Setup(x => x.Orders)
                .Returns(_orderRepository.Object);

            _handler = new GetOrderQueryHandler(
                _unitOfWork.Object,
                _mapper.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnOrder()
        {
            var order = new Order(
                "123",
                "Juan",
                "LAB",
                "Laboratorio",
                OrderPriority.Normal);

            var expected = new OrderDto(
                order.Id,
                order.PatientId,
                order.PatientName,
                order.ServiceCode,
                order.ServiceDescription,
                order.Priority,
                order.Status,
                order.CreatedAt,
                order.ProcessedAt);

            _orderRepository
                .Setup(x => x.GetByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(order);

            _mapper
                .Setup(x => x.Map<OrderDto>(order))
                .Returns(expected);

            var result = await _handler.Handle(
                new GetOrderQuery(order.Id),
                CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task Handle_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            _unitOfWork.Setup(x =>
                    x.Orders.GetByIdAsync(
                        It.IsAny<Guid>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync((Order?)null);

            var result = await _handler.Handle(
                new GetOrderQuery(Guid.NewGuid()),
                CancellationToken.None);

            result.Should().BeNull();
        }
    }
}