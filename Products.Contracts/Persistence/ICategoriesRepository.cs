using Products.Contracts.Domain;

namespace Products.Contracts.Persistence
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryByName(string categoryName);
    }
}
