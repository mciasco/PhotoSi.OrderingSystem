using FluentValidation;
using Users.Contracts.Domain;
using Users.Contracts.Persistence;

namespace Users.WebApi.Application
{
    public class GetAccountsByUsernameCommandHandler 
        : BaseCommandHandlerWithInputWithOutput<GetAccountsByUsernameCommandInput, IEnumerable<Account>, GetAccountsByUsernameCommandInputValidator>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountsByUsernameCommandHandler(
            IAccountsRepository usersRepository,
            IUnitOfWork unitOfWork,
            GetAccountsByUsernameCommandInputValidator validator) : base(validator)
        {
            this._accountsRepository = usersRepository;
            this._unitOfWork = unitOfWork;
        }

        protected override async Task<IEnumerable<Account>> OnExecute(GetAccountsByUsernameCommandInput input)
        {
            return await _accountsRepository.GetAccountsByUsername(input.Username);
        }
    }

    public class GetAccountsByUsernameCommandInput
    {
        public string Username { get; set; }
    }

    public class GetAccountsByUsernameCommandInputValidator : AbstractValidator<GetAccountsByUsernameCommandInput>
    {
        public GetAccountsByUsernameCommandInputValidator()
        {
            RuleFor(i => i.Username).NotEmpty().WithMessage("Cannot retrieve accounts by empty username");
        }
    }
}
