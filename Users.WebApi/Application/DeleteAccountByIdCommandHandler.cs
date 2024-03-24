using Commons.WebApi.Application;
using FluentValidation;
using Users.Contracts.Clients;
using Users.Contracts.Persistence;
using Commons.Contracts.Persistence;

namespace Products.WebApi.Application
{
    public class DeleteAccountByIdCommandHandler : BaseCommandHandlerWithInputWithOutput<DeleteAccountByIdCommandInput, bool, DeleteAccountByIdCommandInputValidator>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IAddressBookServiceClient _addressBookServiceClient;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountByIdCommandHandler(
            IAccountsRepository accountsRepository, 
            IAddressBookServiceClient addressBookServiceClient,
            IUnitOfWork unitOfWork,
            DeleteAccountByIdCommandInputValidator validator) : base(validator)
        {
            this._accountsRepository = accountsRepository;
            this._addressBookServiceClient = addressBookServiceClient;
            this._unitOfWork = unitOfWork;
        }

        protected override async Task<bool> OnExecute(DeleteAccountByIdCommandInput input)
        {
            var accountToDelete = await _accountsRepository.GetAccountById(input.AccountId);
            if (accountToDelete is null)
                throw new ArgumentException($"No user account found by id {input}");

            var deleteAddressesSuccess = await _addressBookServiceClient.DeleteAllAddressByAccount(input.AccountId);
            if (deleteAddressesSuccess)
                await _accountsRepository.DeleteAccount(accountToDelete);

            return await _unitOfWork.SaveChangesAsync() == 1;
        }
    }

    public class DeleteAccountByIdCommandInput
    {
        public string AccountId { get; set; }
    }


    public class DeleteAccountByIdCommandInputValidator : AbstractValidator<DeleteAccountByIdCommandInput>
    {
        public DeleteAccountByIdCommandInputValidator()
        {
            RuleFor(i => i.AccountId).NotEmpty().WithMessage("Cannot delete account by null id");
        }
    }
}
