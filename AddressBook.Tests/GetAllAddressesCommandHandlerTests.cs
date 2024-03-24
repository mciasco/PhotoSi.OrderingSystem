using AddressBook.Contracts.Domain;
using AddressBook.Contracts.Persistence;
using AddressBook.WebApi.Application;
using Moq;
using Commons.Contracts.Persistence;

namespace AddressBook.Tests
{
    public class GetAllAddressesCommandHandlerTests
    {
        private readonly Mock<IAddressesRepository> _addressesRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetAllAddressesCommandHandler _handler;


        public GetAllAddressesCommandHandlerTests()
        {
            _addressesRepositoryMock = new Mock<IAddressesRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _handler = new GetAllAddressesCommandHandler(
                _addressesRepositoryMock.Object, _unitOfWorkMock.Object);
        }


        [Fact]
        public async Task HandlerReturnsEmptyAddressCollection()
        {
            _addressesRepositoryMock.Setup(r => r.GetAllAddresses()).ReturnsAsync(Enumerable.Empty<Address>());

            var cmdResult = await _handler.Execute();

            Assert.Empty(cmdResult);
            _addressesRepositoryMock.Verify(r => r.GetAllAddresses(), Times.Exactly(1));
            _unitOfWorkMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(0));
        }

        [Fact]
        public async Task HandlerReturnsNotEmptyAddressCollection()
        {
            _addressesRepositoryMock.Setup(r => r.GetAllAddresses()).ReturnsAsync(new Address[]
            {
                Address.Create("AccountId1", "AddressName1", "Country1", "Province1", "City1", "PostalCode1", "StreetName1", "StreetNumber1", true),
                Address.Create("AccountId2", "AddressName2", "Country2", "Province2", "City2", "PostalCode2", "StreetName2", "StreetNumber2", false)
            });

            var cmdResult = await _handler.Execute();

            Assert.NotEmpty(cmdResult);
            Assert.True(cmdResult.Count() == 2);
            _addressesRepositoryMock.Verify(r => r.GetAllAddresses(), Times.Exactly(1));
            _unitOfWorkMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(0));
        }
    }
}