using Orders.Contracts.Clients;
using System.Net.Http.Json;

namespace Orders.Infrastructure.Clients
{
    public class HttpAddressBookServiceClient : IAddressBookServiceClient
    {
        private readonly HttpClient _httpClient;

        public HttpAddressBookServiceClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<AddressClientDto>> GetAddressByAccountId(string accountId)
        {
            var addressDtos = await _httpClient.GetFromJsonAsync<IEnumerable<AddressClientDto>>($"api/addresses/accounts/{accountId}");
            return addressDtos; 
        }
    }
}
