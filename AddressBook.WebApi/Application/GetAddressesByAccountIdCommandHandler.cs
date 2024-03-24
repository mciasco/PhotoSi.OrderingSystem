using AddressBook.Contracts.Domain;
using AddressBook.Contracts.Persistence;
using Commons.WebApi.Application;

namespace AddressBook.WebApi.Application
{
    public class GetAddressesByAccountIdCommandHandler : BaseCommandHandlerWithInputWithOutput<string, IEnumerable<Address>>
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAddressesByAccountIdCommandHandler(IAddressesRepository addressesRepository, IUnitOfWork unitOfWork)
        {
            this._addressesRepository = addressesRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<IEnumerable<Address>> Execute(string input)
        {
            return await _addressesRepository.GetAddressesByAccountId(input);
        }
    }

    
}
