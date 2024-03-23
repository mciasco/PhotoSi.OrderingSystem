using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Contracts.Clients
{
    public interface IAddressBookServiceClient
    {
        Task<AddressClientDto> AddAddress(AddAddressClientDto addAddressClientDto);
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

    public class AddAddressClientDto
    {
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
