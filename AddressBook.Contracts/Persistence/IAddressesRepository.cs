using AddressBook.Contracts.Domain;

namespace AddressBook.Contracts.Persistence
{
    public interface IAddressesRepository
    {
        Task<IEnumerable<Address>> GetAllAddresses();
    }
}
