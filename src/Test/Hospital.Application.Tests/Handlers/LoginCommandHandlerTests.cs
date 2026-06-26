using Hospital.Application.Commands.Login;
using Hospital.Application.UnitOfWork;
using Hospital.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;

namespace Hospital.Application.Tests.Handlers
{
    [TestFixture]
    public class LoginCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWork = null!;
        private Mock<IConfiguration> _config = null!;
        private Mock<ILogger<LoginCommandHandler>> _logger = null!;

        private LoginCommandHandler _handler = null!;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _config = new Mock<IConfiguration>();
            _logger = new Mock<ILogger<LoginCommandHandler>>();

            _config.Setup(x => x["Jwt:Key"])
                .Returns("ThisIsAVeryLongSecretKeyForJwt123456");

            _config.Setup(x => x["Jwt:Issuer"])
                .Returns("Hospital");

            _config.Setup(x => x["Jwt:Audience"])
                .Returns("HospitalApi");

            _handler = new LoginCommandHandler(
                _unitOfWork.Object,
                _config.Object,
                _logger.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnToken_WhenUserExists()
        {
            var user = new User("QVision", "123456");

            _unitOfWork.Setup(x =>
                x.Users.GetByUsernameAsync(
                    "QVision",
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var command = new LoginCommand(
                "QVision",
                "123456");

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Username.Should().Be("QVision");
            result.PasswordHash.Should().Be("*****");
            result.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public void Handle_ShouldThrowUnauthorized_WhenUserDoesNotExist()
        {
            _unitOfWork.Setup(x =>
                x.Users.GetByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            var command = new LoginCommand(
                "QVision",
                "123456");

            Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}
