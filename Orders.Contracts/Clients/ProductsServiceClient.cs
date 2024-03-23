using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Contracts.Clients
{
    public interface IProductsServiceClient
    {
        Task<ProductClientDto> GetProductById(string productId);
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
}
