using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Contracts.Domain
{
    public class Address
    {
        protected Address()
        {
        }

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

        public static Address Create(
            string ownerAccountId,
            string name,
            string country,
            string stateProvince,
            string city,
            string postalCode,
            string streetName,
            string streetNumber,
            bool isMainAddress)
        {
            return new Address
            {
                AddressId = $"ADR_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}",
                OwnerAccountId = ownerAccountId,
                AddressName = name,
                Country = country,
                StateProvince = stateProvince,
                City = city,
                PostalCode = postalCode,
                StreetName = streetName,
                StreetNumber = streetNumber,
                IsMainAddress = isMainAddress
            };
        }
    }
}
