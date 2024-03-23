
using BackOffice.Contracts.Clients;
using BackOffice.WebApi.Application;
using BackOffice.WebApi.Models;

namespace BackOffice.WebApi.Controllers
{
    public static class ControllersHelpers
    {
        public static AddressApiDto ToApiDto(this AddressClientDto address)
        {
            return new AddressApiDto()
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


        public static AccountApiDto ToApiDto(this AccountClientDto address)
        {
            return new AccountApiDto()
            {
                AccountId = address.AccountId,
                Name = address.Name,
                RegistrationEmail = address.RegistrationEmail,
                Surname = address.Surname,
                Username = address.Username 
            };
        }

        public static ProductApiDto ToApiDto(this ProductClientDto product)
        {
            return new ProductApiDto()
            {
                Id = product.Id,
                CategoryName = product.CategoryName,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                QtyStock = product.QtyStock,
            };
        }

        public static CategoryApiDto ToApiDto(this CategoryClientDto category)
        {
            return new CategoryApiDto()
            {
                Name = category.Name,
                Description = category.Description,
            };
        }


        public static CreateProductCommandInput ToCommandInput(this CreateProductApiDto inputDto)
        {
            var cmdInput = new CreateProductCommandInput();
            cmdInput.Name = inputDto.Name;
            cmdInput.Description = inputDto.Description;
            cmdInput.CategoryName = inputDto.CategoryName;
            cmdInput.QtyStock = inputDto.QtyStock;
            cmdInput.Price = inputDto.Price;

            return cmdInput;
        }

        public static CreateAccountCommandInput ToCommandInput(this CreateAccountApiDto inputDto)
        {
            return new CreateAccountCommandInput()
            {
                Name = inputDto.Name,
                Surname = inputDto.Surname,
                RegistrationEmail = inputDto.RegistrationEmail,
                Username = inputDto.Username,
                Password = inputDto.Password,
                MainShippingAddress = new CreateAccountCommandInputMainShippingAddress()
                {
                    AddressName = inputDto.MainShippingAddress.AddressName,
                    Country = inputDto.MainShippingAddress.Country,
                    StateProvince = inputDto.MainShippingAddress.StateProvince,
                    City = inputDto.MainShippingAddress.City,
                    PostalCode = inputDto.MainShippingAddress.PostalCode,
                    StreetName = inputDto.MainShippingAddress.StreetName,
                    StreetNumber = inputDto.MainShippingAddress.StreetNumber,
                }
            };
        }
    }
}
