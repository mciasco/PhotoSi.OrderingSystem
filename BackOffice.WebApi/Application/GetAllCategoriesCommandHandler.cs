using BackOffice.Contracts.Clients;

namespace BackOffice.WebApi.Application
{
    public class GetAllCategoriesCommandHandler : BaseCommandHandlerNoInputWithOutput<IEnumerable<CategoryClientDto>>
    {
        private readonly IProductsServiceClient _productsServiceClient;

        public GetAllCategoriesCommandHandler(IProductsServiceClient productsServiceClient)
        {
            this._productsServiceClient = productsServiceClient;
        }

        public override async Task<IEnumerable<CategoryClientDto>> Execute()
        {
            return await _productsServiceClient.GetAllCategories();
        }
    }
}
