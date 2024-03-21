namespace AddressBook.WebApi.Models
{
    public class AddressDto
    {
        public string AddressId { get; set; }
        public string OwnerAccountId { get; set; }
        public string AddressName { get; set; }
        public string Country { get; set; }
        public string StateProvice { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public bool IsMainAddress { get; set; }

    }
}
