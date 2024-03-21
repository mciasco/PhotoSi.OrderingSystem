using AddressBook.Contracts.Domain;
using AddressBook.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Infrastructure.Persistence
{
    public class EFCoreAddressesRepository : IAddressesRepository
    {
        private readonly UsersDbContext _dbContext;

        public EFCoreAddressesRepository(UsersDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Address>> GetAllAddresses()
        {
            return await _dbContext
                .Addresses
                .ToArrayAsync();
        }
    }

}
