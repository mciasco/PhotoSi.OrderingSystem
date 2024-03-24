using AddressBook.Contracts.Domain;
using AddressBook.Contracts.Persistence;
using Commons.WebApi.Application;
using Commons.Contracts.Persistence;

namespace AddressBook.WebApi.Application
{
    public class AddAddressCommandHandler : BaseCommandHandlerWithInputWithOutput<AddAddressCommandInput, Address>
    {
        private readonly IAddressesRepository _addressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddAddressCommandHandler(
            IAddressesRepository addressRepository,
            IUnitOfWork unitOfWork)
        {
            this._addressRepository = addressRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<Address> Execute(AddAddressCommandInput input)
        {
            var address = Address.Create(
                input.OwnerAccountId,
                input.AddressName,
                input.Country,
                input.StateProvince,
                input.City,
                input.PostalCode,
                input.StreetName,
                input.StreetNumber,
                input.IsMainAddress);

            await _addressRepository.AddAddress(address);
            
            await _unitOfWork.SaveChangesAsync();

            return address;
        }
    }

    public class AddAddressCommandInput
    {
        public string OwnerAccountId { get; set; }
        public string AddressName { get; set; }
        public string Country { get; set; }
        public string StateProvince { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public bool IsMainAddress { get; set; }
    }
}
