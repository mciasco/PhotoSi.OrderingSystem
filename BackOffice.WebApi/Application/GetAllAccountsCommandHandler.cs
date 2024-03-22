using BackOffice.Contracts.Clients;

namespace BackOffice.WebApi.Application
{
    public class GetAllAccountsCommandHandler : BaseCommandHandlerNoInputWithOutput<IEnumerable<AccountDto>>
    {
        private readonly IUsersServiceClient _usersServiceClient;

        public GetAllAccountsCommandHandler(IUsersServiceClient usersServiceClient)
        {
            this._usersServiceClient = usersServiceClient;
        }

        public override async Task<IEnumerable<AccountDto>> Execute()
        {
            return await _usersServiceClient.GetAllAccounts();
        }
    }
}
