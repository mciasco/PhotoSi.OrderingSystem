using BackOffice.Contracts.Clients;
using Commons.WebApi.Application;


namespace BackOffice.WebApi.Application
{
    public class CreateAccountCommandHandler : BaseCommandHandlerWithInputWithOutput<CreateAccountCommandInput, AccountClientDto>
    {
        private readonly IUsersServiceClient _usersServiceClient;

        public CreateAccountCommandHandler(IUsersServiceClient usersServiceClient)
        {
            this._usersServiceClient = usersServiceClient;
        }

        public override async Task<AccountClientDto> Execute(CreateAccountCommandInput input)
        {
            
            var registerNewAccountClientDto = new RegisterNewAccountClientDto();
            registerNewAccountClientDto.Name = input.Name;
            registerNewAccountClientDto.Surname = input.Surname;
            registerNewAccountClientDto.RegistrationEmail = input.RegistrationEmail;
            registerNewAccountClientDto.Username = input.Username;
            registerNewAccountClientDto.Password = input.Password;
            registerNewAccountClientDto.MainShippingAddress = new RegisterNewAccountMainShippingAddressClientDto()
            {
                AddressName = input.MainShippingAddress.AddressName,
                Country = input.MainShippingAddress.Country,
                StateProvince = input.MainShippingAddress.StateProvince,
                City = input.MainShippingAddress.City,
                PostalCode = input.MainShippingAddress.PostalCode,
                StreetName = input.MainShippingAddress.StreetName,
                StreetNumber = input.MainShippingAddress.StreetNumber,
            };

            var accountDto = await _usersServiceClient.RegisterNewAccount(registerNewAccountClientDto);
            if (accountDto is null)
                throw new Exception("Error while creating a new account");

            return accountDto;
        }
    }

    public class CreateAccountCommandInput
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public CreateAccountCommandInputMainShippingAddress MainShippingAddress { get; set; }

    }

    public class CreateAccountCommandInputMainShippingAddress
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
