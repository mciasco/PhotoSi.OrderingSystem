using NSubstitute;
using Users.Contracts.Clients;
using Users.Contracts.Domain;
using Users.Contracts.Persistence;
using Users.WebApi.Application;

namespace Users.Tests
{
    public class ApplicationTests
    {
        [Fact]
        public async Task Test_Register_New_Account()
        {
            // TODO
            //// arrange mocks
            //var mockAccountsRepository = Substitute.For<IAccountsRepository>();
            //var mockAddressBookServiceClient = Substitute.For<IAddressBookServiceClient>();
            //var mockAccountPasswordHasher = Substitute.For<IAccountPasswordHasher>();
            //var mockUnitOfWork = Substitute.For<IUnitOfWork>();

            //mockAccountPasswordHasher.HashPassword("testPassword").Returns("testPasswordHashed");
            //mockAccountsRepository.AddAccount()

            //mockUnitOfWork.SaveChangesAsync().Returns(Task.FromResult(0));
            //mockUnitOfWork.SaveChanges().Returns(0);

            //// arrage command under test
            //var registerNewAccountCommandHandler = new RegisterNewAccountCommandHandler()

            //// act
            //var accounts = await getAllAddressCommandHandler.Execute();

            //// assert
            //Assert.Empty(accounts);
        }
    }
}