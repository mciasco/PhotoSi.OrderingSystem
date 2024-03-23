using Microsoft.EntityFrameworkCore;
using Users.Contracts.Domain;
using Users.Contracts.Persistence;

namespace Users.Infrastructure.Persistence
{
    public class EFCoreAccountsRepository : IAccountsRepository
    {
        private readonly UsersDbContext _dbContext;

        public EFCoreAccountsRepository(UsersDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddAccount(Account account)
        {
            _dbContext.Accounts.Add(account);
        }

        public async Task DeleteAccount(Account account)
        {
            _dbContext.Accounts.Remove(account);
        }

        public async Task<Account> GetAccountById(string input)
        {
            return await _dbContext.Accounts.FindAsync(input);
        }

        public async Task<IEnumerable<Account>> GetAccountsByUsername(string input)
        {
            return await _dbContext
                .Accounts
                .Where(a => EF.Functions.Like(a.Username, $"%{input}%"))
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _dbContext
                .Accounts
                .ToArrayAsync();
        }
    }
}
