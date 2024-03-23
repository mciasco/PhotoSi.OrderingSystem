using Users.Contracts.Domain;
using Users.WebApi.Application;
using Users.WebApi.Models;

namespace Users.WebApi.Controllers
{
    public static class ControllersHelpers
    {
        public static AccountApiDto ToApiDto(this Account account)
        {
            return new AccountApiDto()
            {
                AccountId = account.AccountId,
                Name = account.Name,
                RegistrationEmail = account.RegistrationEmail,
                Surname = account.Surname,
                Username = account.Username,
            };
        }

        public static RegisterNewAccountCommandInput ToCommandInput(this RegisterNewAccountApiDto apiDto) 
        {
            return new RegisterNewAccountCommandInput()
            {
                Name = apiDto.Name,
                RegistrationEmail = apiDto.RegistrationEmail,
                Password = apiDto.Password,
                Surname = apiDto.Surname,
                Username = apiDto.Username,
                MainShippingAddress = new RegisterNewAccountMainShippingAddressCommandInput()
                {
                    AddressName = apiDto.MainShippingAddress.AddressName,
                    Country = apiDto.MainShippingAddress.Country,
                    StateProvince = apiDto.MainShippingAddress.StateProvince,
                    City = apiDto.MainShippingAddress.City,
                    PostalCode = apiDto.MainShippingAddress.PostalCode,
                    StreetName = apiDto.MainShippingAddress.StreetName,
                    StreetNumber = apiDto.MainShippingAddress.StreetNumber,
                }
            };
        }
    }


}
