namespace BackOffice.Contracts.Clients
{
    public interface IUsersServiceClient
    {
        Task<IEnumerable<AccountDto>> GetAllAccounts();
    }

    public class AccountDto
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationEmail { get; set; }
        public string Username { get; set; }
    }
}
