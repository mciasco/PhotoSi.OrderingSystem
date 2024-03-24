using Users.Contracts.Persistence;
using Commons.Contracts.Persistence;

namespace Users.Infrastructure.Persistence
{
    public class EFCoreUnitOfWork : IUnitOfWork
    {
        private readonly UsersDbContext _dbContext;

        public EFCoreUnitOfWork(UsersDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

}
