﻿using BackOffice.Contracts.Clients;
using Commons.WebApi.Application;

namespace BackOffice.WebApi.Application
{
    public class CreateProductCommandHandler : BaseCommandHandlerWithInputWithOutput<CreateProductCommandInput, ProductClientDto>
    {
        private readonly IProductsServiceClient _productsServiceClient;

        public CreateProductCommandHandler(IProductsServiceClient productsServiceClient)
        {
            this._productsServiceClient = productsServiceClient;
        }

        public override async Task<ProductClientDto> Execute(CreateProductCommandInput input)
        {
            var createNewProductDto = new CreateNewProductClientDto();
            createNewProductDto.Name = input.Name;
            createNewProductDto.Description = input.Description;
            createNewProductDto.CategoryName = input.CategoryName;
            createNewProductDto.QtyStock = input.QtyStock;
            createNewProductDto.Price = input.Price;

            return await _productsServiceClient.CreateNewProduct(createNewProductDto);
        }
    }



    public class CreateProductCommandInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int QtyStock { get; set; }
        public decimal Price { get; set; }
    }
}
