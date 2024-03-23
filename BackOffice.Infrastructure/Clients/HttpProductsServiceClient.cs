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

        public async Task<ProductClientDto> CreateNewProduct(CreateNewProductClientDto createNewProductDto)
        {
            var response = await _httpClient.PostAsJsonAsync<CreateNewProductClientDto>($"api/products", createNewProductDto);
            var productDto = await response.Content.ReadFromJsonAsync<ProductClientDto>();
            return productDto;
        }

        public async Task<string> DeleteProductById(string input)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{input}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<IEnumerable<CategoryClientDto>> GetAllCategories()
        {
            var categoryDtos = await _httpClient.GetFromJsonAsync<IEnumerable<CategoryClientDto>>($"api/categories");
            return categoryDtos;
        }

        public async Task<IEnumerable<ProductClientDto>> GetAllProducts()
        {
            var productDtos = await _httpClient.GetFromJsonAsync<IEnumerable<ProductClientDto>>($"api/products");
            return productDtos;
        }
    }
}
