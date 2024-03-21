using AddressBook.Contracts.Persistence;

namespace AddressBook.Infrastructure.Persistence
{
    public class EFCoreAddressesRepository : IAddressesRepository
    {
        private readonly UsersDbContext _dbContext;

        public EFCoreAddressesRepository(UsersDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
    }

}
