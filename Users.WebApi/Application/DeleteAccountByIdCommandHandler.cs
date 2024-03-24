using Users.Contracts.Clients;
using Users.Contracts.Persistence;
using Users.WebApi.Application;

namespace Products.WebApi.Application
{
    public class DeleteAccountByIdCommandHandler : BaseCommandHandlerWithInputWithOutput<string, bool>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IAddressBookServiceClient _addressBookServiceClient;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountByIdCommandHandler(
            IAccountsRepository accountsRepository, 
            IAddressBookServiceClient addressBookServiceClient,
            IUnitOfWork unitOfWork)
        {
            this._accountsRepository = accountsRepository;
            this._addressBookServiceClient = addressBookServiceClient;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<bool> Execute(string input)
        {
            var accountToDelete = await _accountsRepository.GetAccountById(input);
            if (accountToDelete is null)
                throw new ArgumentException($"No user account found by id {input}");

            var deleteAddressesSuccess = await _addressBookServiceClient.DeleteAllAddressByAccount(input);
            if (deleteAddressesSuccess)
                await _accountsRepository.DeleteAccount(accountToDelete);

            return await _unitOfWork.SaveChangesAsync() == 1;
        }
    }
}
