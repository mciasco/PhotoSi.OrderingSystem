using BackOffice.Contracts.Clients;
using System.Net.Http.Json;

namespace BackOffice.Infrastructure.Clients
{
    public class HttpAddressBookServiceClient : IAddressBookServiceClient
    {
        private readonly HttpClient _httpClient;

        public HttpAddressBookServiceClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<AddressClientDto>> GetAllAddresses()
        {
            var addressDtos = await _httpClient.GetFromJsonAsync<IEnumerable<AddressClientDto>>($"api/addresses");
            return addressDtos; 
        }
    }
}
