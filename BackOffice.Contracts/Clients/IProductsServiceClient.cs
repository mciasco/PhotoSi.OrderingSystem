using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Contracts.Clients
{
    public interface IProductsServiceClient
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<IEnumerable<CategoryDto>> GetAllCategories();
    }
    
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int QtyStock { get; set; }
        public decimal Price { get; set; }
    }

    public class CategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
