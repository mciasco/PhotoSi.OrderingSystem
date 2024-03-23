using AddressBook.Contracts.Domain;
using AddressBook.Contracts.Persistence;
using AddressBook.WebApi.Application;
using NSubstitute;

namespace AddressBook.Tests
{
    public class ApplicationTests
    {
        [Fact]
        public async Task Test_GetAllAddressesCommandHandler_Returns_No_Addresses()
        {
            // arrange mocks
            var mockAddressRepository = Substitute.For<IAddressesRepository>();
            mockAddressRepository.GetAllAddresses().Returns(Enumerable.Empty<Address>());
            var mockUnitOfWork = Substitute.For<IUnitOfWork>();
            mockUnitOfWork.SaveChangesAsync().Returns(Task.FromResult(0));
            mockUnitOfWork.SaveChanges().Returns(0);

            // arrage command under test
            var getAllAddressCommandHandler = new GetAllAddressesCommandHandler(
                mockAddressRepository, mockUnitOfWork);

            // act
            var accounts = await getAllAddressCommandHandler.Execute();

            // assert
            Assert.Empty(accounts);
        }



        [Fact]
        public async Task Test_GetAllAddressesCommandHandler_Returns_Some_Addresses()
        {
            // arrange mocks
            var mockAddressRepository = Substitute.For<IAddressesRepository>();
            mockAddressRepository.GetAllAddresses().Returns(new[]
            {
                Address.Create("Account1", "Casa", "Italy", "PU", "Pesaro", "61100", "Viale della Vittoria", "100", true),
                Address.Create("Account1", "Ufficio", "Italy", "PU", "Pesaro", "61100", "Via delle Regioni", "200", false),
            });
            var mockUnitOfWork = Substitute.For<IUnitOfWork>();
            mockUnitOfWork.SaveChangesAsync().Returns(Task.FromResult(0));
            mockUnitOfWork.SaveChanges().Returns(0);

            // arrage command under test
            var getAllAddressCommandHandler = new GetAllAddressesCommandHandler(
                mockAddressRepository, mockUnitOfWork);

            // act
            var accounts = await getAllAddressCommandHandler.Execute();

            // assert
            Assert.NotEmpty(accounts);
            Assert.True(accounts.Count() == 2);
            Assert.NotNull(accounts.First());
            Assert.NotNull(accounts.Last());
            Assert.True(accounts.First().OwnerAccountId == "Account1");
            Assert.True(accounts.Last().OwnerAccountId == "Account1");
        }
    }
}