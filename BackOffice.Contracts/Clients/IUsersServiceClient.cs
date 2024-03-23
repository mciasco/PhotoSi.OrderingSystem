namespace BackOffice.Contracts.Clients
{
    public interface IUsersServiceClient
    {
        Task<bool> DeleteAccountById(string input);
        Task<IEnumerable<AccountClientDto>> GetAllAccounts();
        Task<AccountClientDto> RegisterNewAccount(RegisterNewAccountClientDto registerNewAccountClientDto);
    }

    public class AccountClientDto
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationEmail { get; set; }
        public string Username { get; set; }
    }

    public class RegisterNewAccountClientDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RegisterNewAccountMainShippingAddressClientDto MainShippingAddress { get; set; }

    }

    public class RegisterNewAccountMainShippingAddressClientDto
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
