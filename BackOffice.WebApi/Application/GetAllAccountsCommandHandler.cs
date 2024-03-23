using BackOffice.Contracts.Clients;

namespace BackOffice.WebApi.Application
{
    public class GetAllAccountsCommandHandler : BaseCommandHandlerNoInputWithOutput<IEnumerable<AccountClientDto>>
    {
        private readonly IUsersServiceClient _usersServiceClient;

        public GetAllAccountsCommandHandler(IUsersServiceClient usersServiceClient)
        {
            this._usersServiceClient = usersServiceClient;
        }

        public override async Task<IEnumerable<AccountClientDto>> Execute()
        {
            return await _usersServiceClient.GetAllAccounts();
        }
    }
}
