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

        public async Task AddAddress(Address address)
        {
            await _dbContext
                .Addresses
                .AddAsync(address);
        }

        public async Task DeleteAddresses(IEnumerable<Address> addressesByAccount)
        {
            _dbContext.Addresses.RemoveRange(addressesByAccount);
        }

        public async Task<IEnumerable<Address>> GetAddressesByAccountId(string input)
        {
            return await _dbContext
                .Addresses
                .Where(adr => adr.OwnerAccountId == input)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAddresses()
        {
            return await _dbContext
                .Addresses
                .ToArrayAsync();
        }
    }

}
