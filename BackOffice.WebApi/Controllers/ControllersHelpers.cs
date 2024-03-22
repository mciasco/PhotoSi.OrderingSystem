
using BackOffice.WebApi.Application;
using BackOffice.WebApi.Models;

namespace BackOffice.WebApi.Controllers
{
    public static class ControllersHelpers
    {
        public static Models.AddressDto ToAddressDto(this Contracts.Clients.AddressDto address)
        {
            return new Models.AddressDto()
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


        public static Models.AccountDto ToAccountDto(this Contracts.Clients.AccountDto address)
        {
            return new Models.AccountDto()
            {
                AccountId = address.AccountId,
                Name = address.Name,
                RegistrationEmail = address.RegistrationEmail,
                Surname = address.Surname,
                Username = address.Username 
            };
        }

        public static Models.ProductDto ToProductDto(this Contracts.Clients.ProductDto product)
        {
            return new Models.ProductDto()
            {
                Id = product.Id,
                CategoryName = product.CategoryName,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                QtyStock = product.QtyStock,
            };
        }

        public static Models.CategoryDto ToCategoryDto(this Contracts.Clients.CategoryDto category)
        {
            return new Models.CategoryDto()
            {
                Name = category.Name,
                Description = category.Description,
            };
        }


        public static CreateProductCommandInput ToCommandInput(this CreateProductDto inputDto)
        {
            var cmdInput = new CreateProductCommandInput();
            cmdInput.Name = inputDto.Name;
            cmdInput.Description = inputDto.Description;
            cmdInput.CategoryName = inputDto.CategoryName;
            cmdInput.QtyStock = inputDto.QtyStock;
            cmdInput.Price = inputDto.Price;

            return cmdInput;
        }
    }
}
