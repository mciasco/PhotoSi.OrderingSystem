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

        public async Task<AccountClientDto> RegisterNewAccount(RegisterNewAccountClientDto registerNewAccountClientDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/accounts", registerNewAccountClientDto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<AccountClientDto>();
            else
                return null;
        }

        public async Task<IEnumerable<AccountClientDto>> GetAllAccounts()
        {
            var accountDtos = await _httpClient.GetFromJsonAsync<IEnumerable<AccountClientDto>>($"api/accounts");
            return accountDtos;
        }

        public async Task<bool> DeleteAccountById(string input)
        {
            var response = await _httpClient.DeleteAsync($"api/accounts/{input}");
            if (response.IsSuccessStatusCode)
                return bool.Parse(await response.Content.ReadAsStringAsync());
            else
                return false;
        }
    }
}
