using Users.Contracts.Clients;
using System.Net.Http.Json;

namespace Users.Infrastructure.Clients
{
    public class HttpAddressBookServiceClient : IAddressBookServiceClient
    {
        private readonly HttpClient _httpClient;

        public HttpAddressBookServiceClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<AddressClientDto> AddAddress(AddAddressClientDto addAddressDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/addresses", addAddressDto);
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<AddressClientDto>();
            else
                return null;
        }

        public async Task<bool> DeleteAllAddressByAccount(string input)
        {
            var response = await _httpClient.DeleteAsync($"api/addresses/accounts/{input}");
            if(response.IsSuccessStatusCode)
                return bool.Parse(await response.Content.ReadAsStringAsync());
            else
                return false;
        }
    }
}
