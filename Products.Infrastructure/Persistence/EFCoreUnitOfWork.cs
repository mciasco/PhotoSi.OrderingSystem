using Products.Contracts.Persistence;

namespace Products.Infrastructure.Persistence
{
    public class EFCoreUnitOfWork : IUnitOfWork
    {
        private readonly ProductsDbContext _dbContext;

        public EFCoreUnitOfWork(ProductsDbContext dbContext)
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
