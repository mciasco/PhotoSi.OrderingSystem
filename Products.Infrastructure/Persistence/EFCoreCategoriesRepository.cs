using Microsoft.EntityFrameworkCore;
using Products.Contracts.Domain;
using Products.Contracts.Persistence;

namespace Products.Infrastructure.Persistence
{
    public class EFCoreCategoriesRepository : ICategoriesRepository
    {
        private readonly ProductsDbContext _dbContext;

        public EFCoreCategoriesRepository(ProductsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            return await _dbContext.Categories.FindAsync(categoryName);
        }
    }
}
