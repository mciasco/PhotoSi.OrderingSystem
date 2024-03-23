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
    }
}
