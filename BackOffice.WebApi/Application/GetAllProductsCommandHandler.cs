﻿using BackOffice.Contracts.Clients;
using Commons.WebApi.Application;
using Commons.WebApi.Application;

namespace BackOffice.WebApi.Application
{
    public class GetAllProductsCommandHandler : BaseCommandHandlerNoInputWithOutput<IEnumerable<ProductClientDto>>
    {
        private readonly IProductsServiceClient _productsServiceClient;

        public GetAllProductsCommandHandler(IProductsServiceClient productsServiceClient)
        {
            this._productsServiceClient = productsServiceClient;
        }

        public override async Task<IEnumerable<ProductClientDto>> Execute()
        {
            return await _productsServiceClient.GetAllProducts();
        }
    }
}
