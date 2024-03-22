using BackOffice.Contracts.Clients;

namespace BackOffice.WebApi.Application
{
    public class GetAllProductsCommandHandler : BaseCommandHandlerNoInputWithOutput<IEnumerable<ProductDto>>
    {
        private readonly IProductsServiceClient _productsServiceClient;

        public GetAllProductsCommandHandler(IProductsServiceClient productsServiceClient)
        {
            this._productsServiceClient = productsServiceClient;
        }

        public override async Task<IEnumerable<ProductDto>> Execute()
        {
            return await _productsServiceClient.GetAllProducts();
        }
    }
}
