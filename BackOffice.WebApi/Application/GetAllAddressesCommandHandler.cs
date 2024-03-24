
using BackOffice.Contracts.Clients;
using Commons.WebApi.Application;

namespace BackOffice.WebApi.Application
{
    public class GetAllAddressesCommandHandler : BaseCommandHandlerNoInputWithOutput<IEnumerable<AddressClientDto>>
    {
        private readonly IAddressBookServiceClient _addressBookServiceClient;

        public GetAllAddressesCommandHandler(IAddressBookServiceClient addressBookServiceClient)
        {
            this._addressBookServiceClient = addressBookServiceClient;
        }

        public override async Task<IEnumerable<AddressClientDto>> Execute()
        {
            return await _addressBookServiceClient.GetAllAddresses();
        }
    }
}
