using BackOffice.Contracts.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Infrastructure.Clients
{
    public class HttpProductsServiceClient : IProductsServiceClient
    {
        private readonly HttpClient _httpClient;

        public HttpProductsServiceClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var categoryDtos = await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDto>>($"api/categories");
            return categoryDtos;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var productDtos = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"api/products");
            return productDtos;
        }
    }
}
