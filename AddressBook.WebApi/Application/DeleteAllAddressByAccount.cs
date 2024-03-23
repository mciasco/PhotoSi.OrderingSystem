using AddressBook.Contracts.Persistence;

namespace AddressBook.WebApi.Application
{
    public class DeleteAllAddressByAccount : BaseCommandHandlerWithInputWithOutput<string, bool>
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAllAddressByAccount(
            IAddressesRepository addressesRepository,
            IUnitOfWork unitOfWork)
        {
            this._addressesRepository = addressesRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<bool> Execute(string input)
        {
            var addressesByAccount = await _addressesRepository.GetAddressesByAccountId(input);
            await _addressesRepository.DeleteAddresses(addressesByAccount);

            return await _unitOfWork.SaveChangesAsync() == (addressesByAccount.Count());
        }
    }
}
