using Users.Contracts.Domain;

namespace Users.Contracts.Persistence
{
    public interface IAccountsRepository
    {
        Task<Account> GetAccountById(string input);
        Task<IEnumerable<Account>> GetAccountsByUsername(string input);
        Task<IEnumerable<Account>> GetAllAccounts();
        Task AddAccount(Account account);
        Task DeleteAccount(Account account);
    }
}
