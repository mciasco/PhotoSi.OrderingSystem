using Orders.Contracts.Persistence;
using Commons.Contracts.Persistence;

namespace Orders.Infrastructure.Persistence
{
    public class EFCoreUnitOfWork : IUnitOfWork
    {
        private readonly OrdersDbContext _dbContext;

        public EFCoreUnitOfWork(OrdersDbContext dbContext)
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
