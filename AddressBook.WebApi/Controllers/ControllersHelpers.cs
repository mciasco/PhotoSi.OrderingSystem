using AddressBook.Contracts.Domain;
using AddressBook.WebApi.Models;

namespace AddressBook.WebApi.Controllers
{
    public static class ControllersHelpers
    {
        public static AddressDto ToAccountDto(this Address address)
        {
            return new AddressDto()
            {
                AddressId = address.AddressId,
                OwnerAccountId = address.OwnerAccountId,
                AddressName = address.AddressName,
                Country = address.Country,
                StateProvice = address.StateProvice,
                City = address.City,
                PostalCode = address.PostalCode,
                StreetName = address.StreetName,
                StreetNumber = address.StreetNumber,
                IsMainAddress = address.IsMainAddress,
            };
        }
    }
}
