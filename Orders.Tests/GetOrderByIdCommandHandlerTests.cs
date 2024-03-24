using Moq;
using Orders.Contracts.Domain;
using Orders.Contracts.Persistence;
using Orders.WebApi.Application;
using Commons.Contracts.Persistence;


namespace Orders.Tests
{
    public class GetOrderByIdCommandHandlerTests
    {
        private readonly Mock<IOrdersRepository> _ordersRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetOrderByIdCommandHandler _handler;


        public GetOrderByIdCommandHandlerTests()
        {
            _ordersRepositoryMock = new Mock<IOrdersRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _handler = new GetOrderByIdCommandHandler(
                _ordersRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task HandlerThrowsExceptionIfOrderIdIsNull()
        {
            _ordersRepositoryMock.Setup(r => r.GetOrderById(It.Is((string i) => string.IsNullOrEmpty(i))))
                .ReturnsAsync((string i) => null);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Execute(null));
            _ordersRepositoryMock.Verify(r => r.GetOrderById(It.IsAny<string>()), Times.Never());
            _unitOfWorkMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
        }

        [Fact]
        public async Task HandlerThrowsExceptionIfOrderIdIsStringEmpty()
        {
            _ordersRepositoryMock.Setup(r => r.GetOrderById(It.Is((string i) => string.IsNullOrEmpty(i))))
                .ReturnsAsync((string i) => null);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Execute(string.Empty));
            _ordersRepositoryMock.Verify(r => r.GetOrderById(It.IsAny<string>()), Times.Never());
            _unitOfWorkMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
        }

        [Fact]
        public async Task HandlerReturnsNoOrdersIfNoneOfThemMatchInputId()
        {
            _ordersRepositoryMock.Setup(r => r.GetOrderById(It.Is((string i) => i == "TestOrderId")))
                .ReturnsAsync((string i) =>
                {
                    var ord = Order.Create("TestOrderDescr", new List<OrderedProduct>(), "TestAccountId", "TestAddressId");
                    ord.Id = "TestOrderId";
                    return ord;
                });

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            var cmdResult = await _handler.Execute("UnmatchingOrderId");
            Assert.Null(cmdResult);

            _ordersRepositoryMock.Verify(r => r.GetOrderById(It.IsAny<string>()), Times.Once);
            _unitOfWorkMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
        }


        [Fact]
        public async Task HandlerReturnsOrderThatMatchInputId()
        {
            _ordersRepositoryMock.Setup(r => r.GetOrderById(It.Is((string i) => i == "TestOrderId")))
                .ReturnsAsync((string i) =>
                {
                    var ord = Order.Create("TestOrderDescr", new List<OrderedProduct>(), "TestAccountId", "TestAddressId");
                    ord.Id = "TestOrderId";
                    return ord;
                });

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            var cmdResult = await _handler.Execute("TestOrderId");
            Assert.NotNull(cmdResult);
            Assert.True(cmdResult.Id == "TestOrderId");
            Assert.True(cmdResult.Description == "TestOrderDescr");

            _ordersRepositoryMock.Verify(r => r.GetOrderById(It.IsAny<string>()), Times.Once);
            _unitOfWorkMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
        }
    }
}