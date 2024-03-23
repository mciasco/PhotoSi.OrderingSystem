using Products.Contracts.Domain;

namespace Products.Contracts.Persistence
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(string productId);
        Task<Product> GetProductByName(string productName);
        Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);
        Task AddProduct(Product product);
        Task DeleteProduct(string input);
    }
}
