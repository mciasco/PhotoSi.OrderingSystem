using Users.Contracts.Domain;
using Users.Contracts.Persistence;

namespace Users.WebApi.Application
{
    public class GetAccountByIdCommandHandler : BaseCommandHandlerWithInputWithOutput<string, Account>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountByIdCommandHandler(
            IAccountsRepository usersRepository,
            IUnitOfWork unitOfWork)
        {
            this._accountsRepository = usersRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<Account> Execute(string input)
        {
            return await _accountsRepository.GetAccountById(input);
        }
    }


}
