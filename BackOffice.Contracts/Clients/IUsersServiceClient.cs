namespace BackOffice.Contracts.Clients
{
    public interface IUsersServiceClient
    {
        Task<IEnumerable<AccountClientDto>> GetAllAccounts();
        Task<AccountClientDto> CreateNewAccount();
    }

    public class AccountClientDto
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationEmail { get; set; }
        public string Username { get; set; }
    }

    public class CreateNewAccountClientDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public CreateNewAccountShippingAddressClientDto ShippingAddress { get; set; }
    }

    public class CreateNewAccountShippingAddressClientDto
    {

    }
}
