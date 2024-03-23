namespace Orders.Contracts.Clients
{
    public interface IUsersServiceClient
    {
        Task<AccountClientDto> GetAccountById(string accountId);
    }

    public class AccountClientDto
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationEmail { get; set; }
        public string Username { get; set; }
    }
}
