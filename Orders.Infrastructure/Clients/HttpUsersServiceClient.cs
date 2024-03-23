using Orders.Contracts.Clients;
using System.Net.Http.Json;

namespace Orders.Infrastructure.Clients
{
    public class HttpUsersServiceClient : IUsersServiceClient
    {
        private readonly HttpClient _httpClient;

        public HttpUsersServiceClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<AccountClientDto> GetAccountById(string accountId)
        {
            var accountDto = await _httpClient.GetFromJsonAsync<AccountClientDto>($"api/accounts/{accountId}");
            return accountDto;
        }
    }
}
