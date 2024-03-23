namespace Orders.Contracts.Clients
{
    public interface IAddressBookServiceClient
    {
        Task<IEnumerable<AddressClientDto>> GetAddressByAccountId(string accountId);
    }

    public class AddressClientDto
    {
        public string AddressId { get; set; }
        public string OwnerAccountId { get; set; }
        public string AddressName { get; set; }
        public string Country { get; set; }
        public string StateProvince { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public bool IsMainAddress { get; set; }
    }
}
