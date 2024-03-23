using AddressBook.Contracts.Domain;
using AddressBook.WebApi.Application;
using AddressBook.WebApi.Models;
using System.Runtime.CompilerServices;

namespace AddressBook.WebApi.Controllers
{
    public static class ControllersHelpers
    {
        public static AddressDto ToApiDto(this Address address)
        {
            return new AddressDto()
            {
                AddressId = address.AddressId,
                OwnerAccountId = address.OwnerAccountId,
                AddressName = address.AddressName,
                Country = address.Country,
                StateProvince = address.StateProvince,
                City = address.City,
                PostalCode = address.PostalCode,
                StreetName = address.StreetName,
                StreetNumber = address.StreetNumber,
                IsMainAddress = address.IsMainAddress,
            };
        }

        public static AddAddressCommandInput ToCommandInput(this AddAddressApiDto addAddressApiDto)
        {
            return new AddAddressCommandInput()
            {
                OwnerAccountId = addAddressApiDto.OwnerAccountId,
                AddressName = addAddressApiDto.AddressName,
                Country = addAddressApiDto.Country,
                StateProvince = addAddressApiDto.StateProvince,
                City = addAddressApiDto.City,
                PostalCode = addAddressApiDto.PostalCode,
                StreetName = addAddressApiDto.StreetName,
                StreetNumber = addAddressApiDto.StreetNumber,
                IsMainAddress = addAddressApiDto.IsMainAddress,
            };
        }
    }

    
}
