using FluentValidation;
using Users.Contracts.Domain;
using Users.Contracts.Persistence;

namespace Users.WebApi.Application
{
    public class GetAccountByIdCommandHandler : BaseCommandHandlerWithInputWithOutput<GetAccountByIdCommandInput, Account, GetAccountByIdCommandInputValidator>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountByIdCommandHandler(
            IAccountsRepository usersRepository,
            IUnitOfWork unitOfWork,
            GetAccountByIdCommandInputValidator validator) : base(validator)
        {
            this._accountsRepository = usersRepository;
            this._unitOfWork = unitOfWork;
        }

        protected override async Task<Account> OnExecute(GetAccountByIdCommandInput input)
        {
            return await _accountsRepository.GetAccountById(input.AccountId);
        }
    }

    public class GetAccountByIdCommandInput
    {
        public string AccountId { get; set; }
    }


    public class GetAccountByIdCommandInputValidator : AbstractValidator<GetAccountByIdCommandInput>
    {
        public GetAccountByIdCommandInputValidator()
        {
            RuleFor(i => i.AccountId).NotEmpty().WithMessage("Cannot retrieve account by null id");
        }
    }
}
