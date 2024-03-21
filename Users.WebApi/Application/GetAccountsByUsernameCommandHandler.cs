using Users.Contracts.Domain;
using Users.Contracts.Persistence;

namespace Users.WebApi.Application
{
    public class GetAccountsByUsernameCommandHandler : BaseCommandHandlerWithInputWithOutput<string, IEnumerable<Account>>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountsByUsernameCommandHandler(
            IAccountsRepository usersRepository,
            IUnitOfWork unitOfWork)
        {
            this._accountsRepository = usersRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<IEnumerable<Account>> Execute(string input)
        {
            return await _accountsRepository.GetAccountsByUsername(input);
        }
    }
}
