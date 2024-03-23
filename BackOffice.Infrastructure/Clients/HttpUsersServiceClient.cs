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

        public Task<AccountClientDto> CreateNewAccount()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccountClientDto>> GetAllAccounts()
        {
            var accountDtos = await _httpClient.GetFromJsonAsync<IEnumerable<AccountClientDto>>($"api/accounts");
            return accountDtos;
        }
    }
}
