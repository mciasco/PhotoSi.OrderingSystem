using BackOffice.Contracts.Clients;

namespace BackOffice.WebApi.Application
{
    public class DeleteProductCommandHandler : BaseCommandHandlerWithInputWithOutput<string, string>
    {
        private readonly IProductsServiceClient _productsServiceClient;

        public DeleteProductCommandHandler(IProductsServiceClient productsServiceClient)
        {
            this._productsServiceClient = productsServiceClient;
        }

        public override async Task<string> Execute(string input)
        {
            return await _productsServiceClient.DeleteProductById(input);
        }
    }
}
