using BackOffice.Contracts.Clients;
using System.Net.Http.Json;

namespace BackOffice.Infrastructure.Clients
{
    public class HttpUsersServiceClient : IUsersServiceClient
    {
        private readonly HttpClient _httpClient;

        public HttpUsersServiceClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccounts()
        {
            var accountDtos = await _httpClient.GetFromJsonAsync<IEnumerable<AccountDto>>($"api/accounts");
            return accountDtos;
        }
    }
}
