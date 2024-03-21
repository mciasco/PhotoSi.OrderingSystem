using Orders.Contracts.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Clients
{
    public class HttpProductsServiceClient : IProductsServiceClient
    {
        private readonly HttpClient _httpClient;

        public HttpProductsServiceClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<ProductDto> GetProductById(string productId)
        {
            var productDto = await _httpClient.GetFromJsonAsync<ProductDto>($"api/products/{productId}");

            return productDto;
        }
    }
}
