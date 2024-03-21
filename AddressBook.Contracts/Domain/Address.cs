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
        public string StateProvice { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public bool IsMainAddress { get; set; }

    }
}
