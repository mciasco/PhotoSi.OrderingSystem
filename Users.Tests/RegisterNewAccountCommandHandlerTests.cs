using Moq;
using Users.Contracts.Clients;
using Users.Contracts.Domain;
using Users.Contracts.Persistence;
using Users.WebApi.Application;

namespace Users.Tests
{
    public class RegisterNewAccountCommandHandlerTests
    {
        private static RegisterNewAccountCommandInput CreateCommandInput()
        {
            return new RegisterNewAccountCommandInput()
            {
                Name = "TestName",
                Surname = "TestSurname",
                RegistrationEmail = "test@mail.com",
                Username = "testusername",
                Password = "testpassword",
                MainShippingAddress = new RegisterNewAccountMainShippingAddressCommandInput()
                {
                    AddressName = "SomeAddressName",
                    Country = "SomeCountry",
                    StateProvince = "SomeProvince",
                    City = "SomeCity",
                    PostalCode = "SomePostalCode",
                    StreetName = "SomeStreetName",
                    StreetNumber = "666",
                }
            };
        }

        private readonly Mock<IAccountPasswordHasher> _pwdHasherMock;
        private readonly Mock<IAddressBookServiceClient> _addressBookServiceClientMock;
        private readonly Mock<IAccountsRepository> _accountsRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly RegisterNewAccountCommandHandler _handler;


        public RegisterNewAccountCommandHandlerTests()
        {
            _pwdHasherMock = new Mock<IAccountPasswordHasher>();
            _accountsRepositoryMock = new Mock<IAccountsRepository>();
            _addressBookServiceClientMock = new Mock<IAddressBookServiceClient>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _handler = new RegisterNewAccountCommandHandler(
                _accountsRepositoryMock.Object, _addressBookServiceClientMock.Object, _pwdHasherMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task HandlerThrowsExceptionWhenPasswordHashIsStringEmpty()
        {
            var commandInput = CreateCommandInput();
            _pwdHasherMock.Setup(ph => ph.HashPassword(It.Is<string>(p => p == commandInput.Password)))
                .ReturnsAsync((string pwd) => string.Empty);

            await Assert.ThrowsAsync<Exception>(async () => await _handler.Execute(commandInput));
        }

        [Fact]
        public async Task HandlerThrowsExceptionWhenPasswordHashIsNull()
        {
            var commandInput = CreateCommandInput();
            _pwdHasherMock.Setup(ph => ph.HashPassword(It.Is<string>(p => p == commandInput.Password)))
                .ReturnsAsync((string pwd) => null);

            await Assert.ThrowsAsync<Exception>(async () => await _handler.Execute(commandInput));
        }

        [Fact]
        public async Task HandlerThrowsExceptionWhenRegistrationEmailIsNotValid()
        {
            var commandInput = CreateCommandInput();
            commandInput.RegistrationEmail = "invalid_email";

            _pwdHasherMock.Setup(ph => ph.HashPassword(It.Is<string>(p => p == commandInput.Password)))
                .ReturnsAsync((string pwd) => $"HASH({pwd})");

            await Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Execute(commandInput));
        }

        [Fact]
        public async Task HandlerThrowsExceptionWhenNoAccountIsSavedByUnitOfWork()
        {
            var commandInput = CreateCommandInput();

            _pwdHasherMock.Setup(ph => ph.HashPassword(It.Is<string>(p => p == commandInput.Password)))
                .ReturnsAsync((string pwd) => $"HASH({pwd})");

            _accountsRepositoryMock.Setup(r => r.AddAccount(It.IsAny<Account>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            await Assert.ThrowsAsync<Exception>(async () => await _handler.Execute(commandInput));
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(1));
            _accountsRepositoryMock.Verify(r => r.AddAccount(It.IsAny<Account>()), Times.Exactly(1));
        }

        [Fact]
        public async Task HandlerThrowsExceptionWhenAddressBookServiceClientReturnsNullAccountDto()
        {
            var commandInput = CreateCommandInput();

            _pwdHasherMock.Setup(ph => ph.HashPassword(It.Is<string>(p => p == commandInput.Password)))
                .ReturnsAsync((string pwd) => $"HASH({pwd})");

            _accountsRepositoryMock.Setup(r => r.AddAccount(It.IsAny<Account>())).Returns(Task.CompletedTask);
            _accountsRepositoryMock.Setup(r => r.DeleteAccount(It.IsAny<Account>())).Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _addressBookServiceClientMock.Setup(a => a.AddAddress(It.IsAny<AddAddressClientDto>()))
                .ReturnsAsync(default(AddressClientDto));

            await Assert.ThrowsAsync<Exception>(async () => await _handler.Execute(commandInput));
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
            _accountsRepositoryMock.Verify(r => r.AddAccount(It.IsAny<Account>()), Times.Exactly(1));
            _accountsRepositoryMock.Verify(r => r.DeleteAccount(It.IsAny<Account>()), Times.Exactly(1));
            _addressBookServiceClientMock.Verify(r => r.AddAddress(It.IsAny<AddAddressClientDto>()), Times.Exactly(1));
        }


        [Fact]
        public async Task HandlerHappyPath()
        {
            var commandInput = CreateCommandInput();

            _pwdHasherMock.Setup(ph => ph.HashPassword(It.Is<string>(p => p == commandInput.Password)))
                .ReturnsAsync((string pwd) => $"HASH({pwd})");

            _accountsRepositoryMock.Setup(r => r.AddAccount(It.IsAny<Account>())).Returns(Task.CompletedTask);
            _accountsRepositoryMock.Setup(r => r.DeleteAccount(It.IsAny<Account>())).Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            
            _addressBookServiceClientMock.Setup(ac => ac.AddAddress(It.IsAny<AddAddressClientDto>()))
                .ReturnsAsync((AddAddressClientDto input) =>
                {
                    return new AddressClientDto()
                    {
                        AddressId = $"ADR_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}",
                        OwnerAccountId = input.OwnerAccountId,
                        AddressName = input.AddressName,
                        Country = input.Country,
                        StateProvince = input.StateProvince,
                        City = input.City,
                        PostalCode = input.PostalCode,
                        StreetName = input.StreetName,
                        StreetNumber = input.StreetNumber,
                        IsMainAddress = input.IsMainAddress,
                    };
                });


            var cmdResult = await _handler.Execute(commandInput);

            Assert.NotNull(cmdResult);
            Assert.NotNull(cmdResult.AccountId);
            Assert.Equal(commandInput.Name, cmdResult.Name);
            Assert.Equal(commandInput.Surname, cmdResult.Surname);
            Assert.Equal(commandInput.RegistrationEmail, cmdResult.RegistrationEmail);
            Assert.Equal($"HASH({commandInput.Password})", cmdResult.PasswordHash);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(1));
            _accountsRepositoryMock.Verify(r => r.AddAccount(It.IsAny<Account>()), Times.Exactly(1));
            _accountsRepositoryMock.Verify(r => r.DeleteAccount(It.IsAny<Account>()), Times.Exactly(0));
            _addressBookServiceClientMock.Verify(r => r.AddAddress(It.IsAny<AddAddressClientDto>()), Times.Exactly(1));
        }
    }
}