using AddressBook.Contracts.Domain;
using AddressBook.Contracts.Persistence;
using Commons.WebApi.Application;
using Commons.Contracts.Persistence;

namespace AddressBook.WebApi.Application
{
    public class GetAllAddressesCommandHandler : BaseCommandHandlerNoInputWithOutput<IEnumerable<Address>>
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAddressesCommandHandler(IAddressesRepository addressesRepository, IUnitOfWork unitOfWork)
        {
            this._addressesRepository = addressesRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<IEnumerable<Address>> Execute()
        {
            return await _addressesRepository.GetAllAddresses();
        }
    }
}
