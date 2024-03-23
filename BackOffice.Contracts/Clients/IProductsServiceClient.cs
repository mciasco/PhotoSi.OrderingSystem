using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Contracts.Clients
{
    public interface IProductsServiceClient
    {
        Task<IEnumerable<ProductClientDto>> GetAllProducts();
        Task<IEnumerable<CategoryClientDto>> GetAllCategories();
        Task<ProductClientDto> CreateNewProduct(CreateNewProductClientDto createNewProductDto);
        Task<string> DeleteProductById(string input);
    }
    
    public class ProductClientDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int QtyStock { get; set; }
        public decimal Price { get; set; }
    }


    public class CreateNewProductClientDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int QtyStock { get; set; }
        public decimal Price { get; set; }
    }

    public class CategoryClientDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
