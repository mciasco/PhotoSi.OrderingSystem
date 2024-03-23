using BackOffice.Contracts.Clients;

namespace BackOffice.WebApi.Application
{
    public class CreateAccountCommandHandler : BaseCommandHandlerWithInputWithOutput<CreateAccountCommandInput, AccountClientDto>
    {
        private readonly IProductsServiceClient _productsServiceClient;

        public CreateAccountCommandHandler(IProductsServiceClient productsServiceClient)
        {
            this._productsServiceClient = productsServiceClient;
        }

        public override async Task<AccountClientDto> Execute(CreateAccountCommandInput input)
        {
            throw new NotImplementedException();

        }
    }

    public class CreateAccountCommandInput
    {

    }
}
