using FluentValidation;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Users.Contracts.Clients;
using Users.Contracts.Domain;
using Users.Contracts.Persistence;

namespace Users.WebApi.Application
{
    public class RegisterNewAccountCommandHandler : BaseCommandHandlerWithInputWithOutput<RegisterNewAccountCommandInput, Account, RegisterNewAccountCommandInputValidator>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IAddressBookServiceClient _addressBookServiceClient;
        private readonly IAccountPasswordHasher _accountPasswordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterNewAccountCommandHandler(
            IAccountsRepository accountsRepository,
            IAddressBookServiceClient addressBookServiceClient,
            IAccountPasswordHasher accountPasswordHasher,
            IUnitOfWork unitOfWork,
            RegisterNewAccountCommandInputValidator validator) : base(validator)
        {
            this._accountsRepository = accountsRepository;
            this._addressBookServiceClient = addressBookServiceClient;
            this._accountPasswordHasher = accountPasswordHasher;
            this._unitOfWork = unitOfWork;
        }

        protected override async Task<Account> OnExecute(RegisterNewAccountCommandInput input)
        {
            var passwordHash = await _accountPasswordHasher.HashPassword(input.Password);
            if (string.IsNullOrEmpty(passwordHash))
                throw new Exception("Hashing password error");

            var newAccount = Account.Create(
                input.Name,
                input.Surname,
                input.RegistrationEmail,
                input.Username,
                passwordHash);
            await _accountsRepository.AddAccount(newAccount);
            var addedEntityNum = await _unitOfWork.SaveChangesAsync();

            if (addedEntityNum != 1)
                throw new Exception("Error saving account to db");
            

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
                throw new Exception("Error registering new account. Cannot add main shipping address");
            }

            return newAccount;
        }
    }

    public class RegisterNewAccountCommandInputValidator : AbstractValidator<RegisterNewAccountCommandInput>
    {
        public RegisterNewAccountCommandInputValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Cannot register account with null name");
            RuleFor(i => i.Surname).NotEmpty().WithMessage("Cannot register account with null surname");
            RuleFor(i => i.RegistrationEmail).NotEmpty().WithMessage("Cannot register account with null registration email");
            RuleFor(i => i.Username).NotEmpty().WithMessage("Cannot register account with null username");
            RuleFor(i => i.Password).NotEmpty().WithMessage("Cannot register account with null password");
            RuleFor(i => i.Password).MinimumLength(6).WithMessage("Account password must be at least 6 chars");
            RuleFor(i => i.MainShippingAddress).NotNull()
                .SetValidator(new RegisterNewAccountMainShippingAddressCommandInputValidator());
        }
    }

    public class RegisterNewAccountMainShippingAddressCommandInputValidator : AbstractValidator<RegisterNewAccountMainShippingAddressCommandInput>
    {
        public RegisterNewAccountMainShippingAddressCommandInputValidator()
        {
            RuleFor(i => i.AddressName).NotEmpty().WithMessage("Address must have a non empty name");
            RuleFor(i => i.Country).NotEmpty().WithMessage("Address must have a non empty country");
            RuleFor(i => i.StateProvince).NotEmpty().WithMessage("Address must have a non empty state or province");
            RuleFor(i => i.City).NotEmpty().WithMessage("Address must have a non empty city");
            RuleFor(i => i.PostalCode).NotEmpty().WithMessage("Address must have a non empty postal code");
            RuleFor(i => i.StreetName).NotEmpty().WithMessage("Address must have a non empty street name");
            RuleFor(i => i.StreetNumber).NotEmpty().WithMessage("Address must have a non empty street number");
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
