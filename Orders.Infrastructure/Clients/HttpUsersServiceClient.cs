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

        public async Task<AccountDto> GetAccountById(string accountId)
        {
            var accoutDto = await _httpClient.GetFromJsonAsync<AccountDto>($"api/accounts/{accountId}");
            return accoutDto;
        }
    }
}
