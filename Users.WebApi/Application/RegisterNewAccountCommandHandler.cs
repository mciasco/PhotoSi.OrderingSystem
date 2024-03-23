using Microsoft.AspNetCore.Mvc.TagHelpers;
using Users.Contracts.Clients;
using Users.Contracts.Domain;
using Users.Contracts.Persistence;

namespace Users.WebApi.Application
{
    public class RegisterNewAccountCommandHandler : BaseCommandHandlerWithInputWithOutput<RegisterNewAccountCommandInput, Account>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IAddressBookServiceClient _addressBookServiceClient;
        private readonly IAccountPasswordHasher _accountPasswordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterNewAccountCommandHandler(
            IAccountsRepository usersRepository,
            IAddressBookServiceClient addressBookServiceClient,
            IAccountPasswordHasher accountPasswordHasher,
            IUnitOfWork unitOfWork)
        {
            this._accountsRepository = usersRepository;
            this._addressBookServiceClient = addressBookServiceClient;
            this._accountPasswordHasher = accountPasswordHasher;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<Account> Execute(RegisterNewAccountCommandInput input)
        {
            var passwordHash = await _accountPasswordHasher.HashPassword(input.Password);
            var newAccount = Account.Create(
                input.Name,
                input.Surname,
                input.RegistrationEmail,
                input.Username,
                passwordHash);

            await _accountsRepository.AddAccount(newAccount);
            await _unitOfWork.SaveChangesAsync();

            var addAddressDto = new AddAddressClientDto()
            {
                OwnerAccountId = newAccount.AccountId,
                AddressName = input.MainShippingAddress.AddressName,
                Country = input.MainShippingAddress.Country,
                StateProvince = input.MainShippingAddress.StateProvince,
                City = input.MainShippingAddress.City,
                PostalCode = input.MainShippingAddress.PostalCode,
                StreetName = input.MainShippingAddress.StreetName,
                StreetNumber = input.MainShippingAddress.StreetNumber,
                IsMainAddress = true,
            };

            var addressAddedDto = await _addressBookServiceClient.AddAddress(addAddressDto);
            if (addressAddedDto is null)
            {
                // rollback account creato
                await _accountsRepository.DeleteAccount(newAccount);
                await _unitOfWork.SaveChangesAsync();
                throw new Exception("Errore nella registrazione di un nuovo utente. Impossibile registrare l'indirizzo di spedizione principale");
            }

            return newAccount;
        }
    }

    public class RegisterNewAccountCommandInput
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RegisterNewAccountMainShippingAddressCommandInput MainShippingAddress { get; set; }
    }

    public class RegisterNewAccountMainShippingAddressCommandInput
    {
        public string AddressName { get; set; }
        public string Country { get; set; }
        public string StateProvince { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
    }
}
