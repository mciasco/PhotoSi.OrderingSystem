using AddressBook.Contracts.Domain;

namespace AddressBook.Contracts.Persistence
{
    public interface IAddressesRepository
    {
        Task<IEnumerable<Address>> GetAddressesByAccountId(string input);
        Task<IEnumerable<Address>> GetAllAddresses();
        Task AddAddress(Address address);
    }
}
