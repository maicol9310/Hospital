using AutoMapper;
using FluentAssertions;
using Hospital.Application.Commands.Orders.CreateOrder;
using Hospital.Application.Interfaces;
using Hospital.Application.UnitOfWork;
using Hospital.Domain.Orders;
using Microsoft.Extensions.Logging;
using Moq;

namespace Hospital.Application.Tests.Handlers
{
    [TestFixture]
    public class CreateOrderCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWork = null!;
        private Mock<IOrderRepository> _orderRepository = null!;
        private Mock<IMapper> _mapper = null!;
        private Mock<ILogger<CreateOrderCommandHandler>> _logger = null!;

        private CreateOrderCommandHandler _handler = null!;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _orderRepository = new Mock<IOrderRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<CreateOrderCommandHandler>>();

            _unitOfWork
                .Setup(x => x.Orders)
                .Returns(_orderRepository.Object);

            _handler = new CreateOrderCommandHandler(
                _unitOfWork.Object,
                _mapper.Object,
                _logger.Object);
        }

        [Test]
        public async Task Handle_ShouldCreateOrder()
        {
            var command = new CreateOrderCommand(
                "123",
                "Juan",
                "LAB",
                "Laboratorio",
                OrderPriority.Normal);

            var response = new CreateOrderResponse(
                Guid.NewGuid(),
                "123",
                "Juan",
                "LAB",
                "Laboratorio",
                OrderPriority.Normal,
                OrderStatus.Pending,
                DateTime.UtcNow);

            _mapper
                .Setup(x => x.Map<CreateOrderResponse>(It.IsAny<Order>()))
                .Returns(response);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeEquivalentTo(response);

            _orderRepository.Verify(x =>
                x.AddAsync(
                    It.IsAny<Order>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            _unitOfWork.Verify(x =>
                x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}