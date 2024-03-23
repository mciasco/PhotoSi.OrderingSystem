using BackOffice.Contracts.Clients;

namespace BackOffice.WebApi.Application
{
    public class DeleteProductCommandHandler : BaseCommandHandlerWithInputWithOutput<string, bool>
    {
        private readonly IProductsServiceClient _productsServiceClient;

        public DeleteProductCommandHandler(IProductsServiceClient productsServiceClient)
        {
            this._productsServiceClient = productsServiceClient;
        }

        public override async Task<bool> Execute(string input)
        {
            return await _productsServiceClient.DeleteProductById(input);
        }
    }
}
