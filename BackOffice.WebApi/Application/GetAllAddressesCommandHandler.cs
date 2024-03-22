
using BackOffice.Contracts.Clients;

namespace BackOffice.WebApi.Application
{
    public class GetAllAddressesCommandHandler : BaseCommandHandlerNoInputWithOutput<IEnumerable<AddressDto>>
    {
        private readonly IAddressBookServiceClient _addressBookServiceClient;

        public GetAllAddressesCommandHandler(IAddressBookServiceClient addressBookServiceClient)
        {
            this._addressBookServiceClient = addressBookServiceClient;
        }

        public override async Task<IEnumerable<AddressDto>> Execute()
        {
            return await _addressBookServiceClient.GetAllAddresses();
        }
    }
}
