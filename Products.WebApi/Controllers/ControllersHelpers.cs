using Products.Contracts.Domain;
using Products.WebApi.Application;
using Products.WebApi.Models;

namespace Products.WebApi.Controllers
{
    public static class ControllersHelpers
    {
        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Description = product.Description,
                CategoryName = product.Category?.Name,
                Name = product.Name,
                Price = product.Price,
                QtyStock = product.QtyStock
            };
        }

        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto()
            {
                Name = category.Name,
                Description = category.Description,
            };
        }


        public static CreateNewProductCommandInput ToCommandInput(this CreateNewProductDto inputDto)
        {
            var cmdInput = new CreateNewProductCommandInput();
            cmdInput.Name = inputDto.Name;
            cmdInput.Description = inputDto.Description;
            cmdInput.CategoryName = inputDto.CategoryName;
            cmdInput.QtyStock = inputDto.QtyStock;
            cmdInput.Price = inputDto.Price;
                        
            return cmdInput;
        }
    }
}
