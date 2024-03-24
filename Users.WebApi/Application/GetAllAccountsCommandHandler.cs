using Users.Contracts.Domain;
using Users.Contracts.Persistence;
using Commons.WebApi.Application;

namespace Users.WebApi.Application
{
    public class GetAllAccountsCommandHandler : BaseCommandHandlerNoInputWithOutput<IEnumerable<Account>>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAccountsCommandHandler(
            IAccountsRepository usersRepository, 
            IUnitOfWork unitOfWork)
        {
            this._accountsRepository = usersRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<IEnumerable<Account>> Execute()
        {
            return await _accountsRepository.GetAllAccounts();
        }
    }
}
