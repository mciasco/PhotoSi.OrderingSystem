using BackOffice.Contracts.Clients;
using Commons.WebApi.Application;

namespace BackOffice.WebApi.Application
{
    public class DeleteAccountCommandHandler : BaseCommandHandlerWithInputWithOutput<string, bool>
    {
        private readonly IUsersServiceClient _usersServiceClient;

        public DeleteAccountCommandHandler(IUsersServiceClient usersServiceClient)
        {
            this._usersServiceClient = usersServiceClient;
        }

        public override async Task<bool> Execute(string input)
        {
            return await _usersServiceClient.DeleteAccountById(input);
        }
    }
}
