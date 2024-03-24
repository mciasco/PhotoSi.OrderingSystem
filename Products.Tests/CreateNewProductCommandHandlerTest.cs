using Moq;
using Products.Contracts.Domain;
using Products.Contracts.Persistence;
using Products.WebApi.Application;
using Commons.Contracts.Persistence;

namespace Products.Tests
{
    public class CreateNewProductCommandHandlerTest
    {
        private static CreateNewProductCommandInput CreateCommandInput()
        {
            return new CreateNewProductCommandInput()
            {
                Name = "TestPrdName",
                Description = "TestPrdDescr",
                CategoryName = "TestCategoryName",
                Price = 10.0m,
                QtyStock = 100
            };
        }

        private readonly Mock<IProductsRepository> _productsRepositoryMock;
        private readonly Mock<ICategoriesRepository> _categoriesRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateNewProductCommandHandler _handler;


        public CreateNewProductCommandHandlerTest()
        {
            _productsRepositoryMock = new Mock<IProductsRepository>();
            _categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _handler = new CreateNewProductCommandHandler(
                _productsRepositoryMock.Object, _categoriesRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task HandlerThrowsExceptionIfCategoryDoesNotExists()
        {
            _categoriesRepositoryMock.Setup(cr => cr.GetCategoryByName(It.Is<string>((string n) => n == "TestCategoryName")))
                .ReturnsAsync(Category.Create("TestCategoryName", "TestCategoryDescr"));

            var cmdInput = CreateCommandInput();
            cmdInput.CategoryName = "UnmatchingCategoryName";
            await Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Execute(cmdInput));

            _categoriesRepositoryMock.Verify(r => r.GetCategoryByName(It.IsAny<string>()), Times.Once);
            _productsRepositoryMock.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        public async Task HandlerHappyPath()
        {
            _categoriesRepositoryMock.Setup(cr => cr.GetCategoryByName(It.Is<string>((string n) => n == "TestCategoryName")))
                .ReturnsAsync(Category.Create("TestCategoryName", "TestCategoryDescr"));

            var cmdInput = CreateCommandInput();
            cmdInput.CategoryName = "TestCategoryName";
            var cmdResult = await _handler.Execute(cmdInput);

            Assert.NotNull(cmdResult);
            Assert.NotNull(cmdResult.Id);
            Assert.True(cmdResult.Name == cmdInput.Name);
            Assert.NotNull(cmdResult.Category);
            Assert.True(cmdResult.Category.Name == cmdInput.CategoryName);

            _categoriesRepositoryMock.Verify(r => r.GetCategoryByName(It.IsAny<string>()), Times.Once);
            _productsRepositoryMock.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}